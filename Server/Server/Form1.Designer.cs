﻿
using Microsoft.EntityFrameworkCore;
using Server.classes;
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
            base.OnLoad(e);
        }

        private Label label1;
    }
}
