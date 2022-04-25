using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace TextractorForms {
    public static class Interop {
        private static List<object> keepAlive = new List<object>();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ReceiveCallbackNative(IntPtr data, int len);
        public delegate void ReceiveCallback(string data);

        [DllImport("Socket_x86.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Socket_ServerOpen(int port, [MarshalAs(UnmanagedType.FunctionPtr)]ReceiveCallbackNative recv);
        public static IntPtr Socket_ServerOpenWrapper(int port, ReceiveCallback recv) {
            void recvCallback(IntPtr data, int len) {
                byte[] bytes = new byte[len];
                Marshal.Copy(data, bytes, 0, len);
                recv(Encoding.UTF8.GetString(bytes));
            }

            ReceiveCallbackNative cb = recvCallback;
            keepAlive.Add(cb);
            return Socket_ServerOpen(port, cb);
        }

        [DllImport("Socket_x86.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Socket_ServerUpdate(IntPtr socket);

        [DllImport("Socket_x86.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Socket_ServerClose(IntPtr socket);

        [DllImport("Socket_x86.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool Socket_ServerConnected(IntPtr socket);

        [DllImport("Socket_x86.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Socket_ServerSendAll(IntPtr socket, IntPtr data, int len);
        public static void Socket_ServerSendAllWrapper(IntPtr socket, string data) {
            unsafe {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                fixed (byte* ptr = bytes) {
                    var iptr = new IntPtr(ptr);
                    Socket_ServerSendAll(socket, iptr, bytes.Length);
                    GC.KeepAlive(iptr);
                    GC.KeepAlive(bytes);
                }
            }
        }


        [DllImport("Extractor.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Ext_GetProcessList();
        public static List<(int pid, string path)> Ext_GetProcessListWrapper() {
            var raw = Marshal.PtrToStringUni(Ext_GetProcessList()).Split('|');
            var list = new List<(int pid, string path)>();
            for (int i = 0; i < raw.Length - 1; i += 2) {
                list.Add((int.Parse(raw[i]), raw[i + 1]));
            }
            return list;
        }

        [DllImport("Extractor.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Ext_InjectProcess(int pid);

        [DllImport("Extractor.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Ext_DetachProcess(int pid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ProcessConnectedNative(int pid);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ProcessDisconnectedNative(int pid);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ThreadAddedNative(IntPtr addr, IntPtr name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void ThreadRemovedNative(IntPtr addr, IntPtr name);
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SentenceReceivedNative(IntPtr addr, IntPtr name, IntPtr text);

        public delegate void ProcessConnected(int pid);
        public delegate void ProcessDisconnected(int pid);
        public delegate void ThreadAdded(IntPtr addr, string name);
        public delegate void ThreadRemoved(IntPtr addr, string name);
        public delegate void SentenceReceived(IntPtr addr, string name, string text);

        private static readonly object mtx = new object();
        [DllImport("Extractor.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Ext_Start(
            [MarshalAs(UnmanagedType.FunctionPtr)] ProcessConnectedNative cb1,
            [MarshalAs(UnmanagedType.FunctionPtr)] ProcessDisconnectedNative cb2,
            [MarshalAs(UnmanagedType.FunctionPtr)] ThreadAddedNative cb3,
            [MarshalAs(UnmanagedType.FunctionPtr)] ThreadRemovedNative cb4,
            [MarshalAs(UnmanagedType.FunctionPtr)] SentenceReceivedNative cb5);

        private static readonly Queue<int> processConnectedQueue = new Queue<int>();
        private static readonly Queue<int> processDisconnectedQueue = new Queue<int>();
        private static readonly Queue<(IntPtr addr, string name)> threadAddedQueue = new Queue<(IntPtr addr, string name)>();
        private static readonly Queue<(IntPtr addr, string name)> threadRemovedQueue = new Queue<(IntPtr addr, string name)>();
        private static readonly Queue<(IntPtr addr, string name, string text)> sentenceReceivedQueue = new Queue<(IntPtr addr, string name, string text)>();

        public static void Ext_StartWrapper() {
            string s(IntPtr p) {
                return Marshal.PtrToStringUni(p);
            }

            void processConnected(int pid) {
                lock (mtx) { processConnectedQueue.Enqueue(pid); }
            }

            void processDisconnected(int pid) {
                lock (mtx) { processDisconnectedQueue.Enqueue(pid); }
            }

            void threadAdded(IntPtr addr, IntPtr name) {
                lock (mtx) { threadAddedQueue.Enqueue((addr, s(name))); }
            }

            void threadRemoved(IntPtr addr, IntPtr name) {
                lock (mtx) { threadRemovedQueue.Enqueue((addr, s(name))); }
            }

            void sentenceReceived(IntPtr addr, IntPtr name, IntPtr text) {
                lock (mtx) { sentenceReceivedQueue.Enqueue((addr, s(name), s(text))); }
            }

            // Prevent GC from collecting these delegates
            ProcessConnectedNative cb1 = processConnected;
            ProcessDisconnectedNative cb2 = processDisconnected;
            ThreadAddedNative cb3 = threadAdded;
            ThreadRemovedNative cb4 = threadRemoved;
            SentenceReceivedNative cb5 = sentenceReceived;

            Ext_Start(
                cb1,
                cb2,
                cb3,
                cb4,
                cb5
                );

            keepAlive.Add(cb1);
            keepAlive.Add(cb2);
            keepAlive.Add(cb3);
            keepAlive.Add(cb4);
            keepAlive.Add(cb5);
        }

        public static void SynchronizeTexthook(
            ProcessConnected cb1,
            ProcessDisconnected cb2,
            ThreadAdded cb3,
            ThreadRemoved cb4,
            SentenceReceived cb5) {
            lock (mtx) {
                while (processConnectedQueue.Count > 0)
                    cb1(processConnectedQueue.Dequeue());

                while (processDisconnectedQueue.Count > 0)
                    cb2(processDisconnectedQueue.Dequeue());

                while (threadAddedQueue.Count > 0) {
                    var (addr, name) = threadAddedQueue.Dequeue();
                    cb3(addr, name);
                }

                while (threadRemovedQueue.Count > 0) {
                    var (addr, name) = threadRemovedQueue.Dequeue();
                    cb4(addr, name);
                }

                while (sentenceReceivedQueue.Count > 0) {
                    var (addr, name, text) = sentenceReceivedQueue.Dequeue();
                    cb5(addr, name, text);
                }
            }
        }
    }
}
