using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Server.classes;
using Server.tcpServer;
using System.Diagnostics;
using AppContext = Server.contexts.ApplicationContext;

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
                await new Server.tcpServer.Server().Start(8000);
            });

            base.OnLoad(e);
        }

        private Server.tcpServer.Server server;
        private Label label1;
    }
}
