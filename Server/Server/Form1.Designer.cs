using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Server.classes;
using Server.tcpServer;
using System.Diagnostics;
using AppContext = Server.contexts.ApplicationContext;

using Server.repositories;

namespace Server
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnLoad(EventArgs e)
        {
            Task.Run(async () =>
            {
                server = new Server.tcpServer.Server();
                server.MessageReceived += (object sender, string message) =>
                {
                    label1.Invoke((MethodInvoker)delegate
                    {
                        label1.Text = message;
                    });
                };
                await server.Start(8000);
            });

            base.OnLoad(e);
        }

        private Server.tcpServer.Server server;
        private Label label1;
    }
}
