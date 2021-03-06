﻿using DotNetNuke.Services.Exceptions;
using System;

namespace Dnn.Showcase
{
    partial class View : ModuleBase
    {
        protected new void Page_Load(Object sender, EventArgs e)
        {

            try
            {
                base.Page_Load(sender, e);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }
        }
    }
}
