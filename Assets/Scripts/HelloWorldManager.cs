using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldManager : MonoBehaviour
    {
        private NetworkManager m_NetworkManager;

        void Awake()
        {
            m_NetworkManager = GetComponent<NetworkManager>();
        }

        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!m_NetworkManager.IsClient && !m_NetworkManager.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();

            }

            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (GUILayout.Button("Host")) m_NetworkManager.StartHost(); //Cliente y host
            if (GUILayout.Button("Client")) m_NetworkManager.StartClient(); //Cliente
            if (GUILayout.Button("Server")) m_NetworkManager.StartServer(); //Hostea el server
        }

        void StatusLabels()
        {
            var mode = m_NetworkManager.IsHost ?
                "Host" : m_NetworkManager.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                m_NetworkManager.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }
    }
}