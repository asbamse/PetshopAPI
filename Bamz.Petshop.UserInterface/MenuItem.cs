using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.UserInterface
{
    /**
     * <summary>
     * Menu item structure.
     * </summary>
     **/
    class MenuItem
    {
        public string Message { get; set; }
        public Action Action { get; set; }

        public MenuItem(string message, Action output)
        {
            Message = message;
            Action = output;
        }

        /**
         * <summary>
         * Runs method.
         * </summary>
         **/
        public void Run()
        {
            Action();
        }
    }
}
