#include "jnet.h"

using namespace jnet;

using RecvCallback = void(*)(const uint8_t*, uint32_t);

struct Server : public JServer {
	Server(int port, RecvCallback recv)
		: JServer((uint16_t)port),
		recv(recv)
	{
	}

	bool OnClientConnect(ExternalJClient client) noexcept override {
		clientCount++;
		return true;
	}

	void OnClientDisconnect(ExternalJClient client) noexcept override {
		clientCount--;
	}

	void OnReceive(ExternalJClient client, Json& j) noexcept override {
		recv(j.data(), (uint32_t)j.size());
	}

	size_t clientCount = 0;
	RecvCallback recv;
};

struct Client : public LocalJClient {
	Client(const char* hostname, int port, RecvCallback recv)
		: LocalJClient(hostname, (uint16_t)port),
		recv(recv) {
	}

	void OnReceive(Json& j) noexcept override {
		recv(j.data(), (uint32_t)j.size());
	}

	RecvCallback recv;
};

#define SOCKET_DLL_EXPORT __declspec(dllexport)

extern "C" {
	SOCKET_DLL_EXPORT Server* Socket_ServerOpen(int port, RecvCallback recv) {
		try {
			return new Server(port, recv);
		} catch (jnet::Exception&) {
			return nullptr;
		}
	}

	SOCKET_DLL_EXPORT void Socket_ServerClose(Server* server) {
		delete server;
	}

	SOCKET_DLL_EXPORT void Socket_ServerSendAll(Server* server, const uint8_t* data, uint32_t len) {
		server->SendAll(Bytes(data, data + len));
	}

	SOCKET_DLL_EXPORT void Socket_ServerUpdate(Server* server) {
		server->Update();
	}

	SOCKET_DLL_EXPORT bool Socket_ServerConnected(Server* server) {
		return server->clientCount > 0;
	}

	SOCKET_DLL_EXPORT Client* Socket_ClientOpen(const char* hostname, int port, RecvCallback recv) {
		try {
			return new Client(hostname, port, recv);
		} catch (jnet::Exception&) {
			return nullptr;
		}
	}

	SOCKET_DLL_EXPORT void Socket_ClientClose(Client* client) {
		delete client;
	}

	SOCKET_DLL_EXPORT void Socket_ClientSend(Client* client, const uint8_t* data, uint32_t len) {
		client->Send(Bytes(data, data + len));
	}

	SOCKET_DLL_EXPORT void Socket_ClientUpdate(Client* client) {
		client->Update();
	}
}
