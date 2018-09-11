using System;
using System.Collections.Generic;
using System.Text;

namespace Bamz.Petshop.UserInterface
{
    /**
     * <summary>
     * Menu in console.
     * </summary>
     **/
    class Menu
    {
        internal string name;
        private MenuItem[] _menuItems;
        private bool _isExit;
        private readonly Menu _parent;
        private readonly bool _isSub;

        public Menu(string name, Menu parent = null, bool isSub = false)
        {
            this.name = name;
            this._isExit = false;
            this._parent = parent;
            this._isSub = isSub;
        }

        /**
         * <summary>
         * Adds menu items to menu.
         * </summary>
         **/
        public void SetMenu(MenuItem[] menuItems)
        {
            // Calculate new list size if it is not a sub menu.
            int defaults = 0;
            if (!_isSub)
            {
                defaults = 2;
                if (_parent != null)
                {
                    defaults = 3;
                }
            }

            // Add all MenuItems wanted.
            _menuItems = new MenuItem[menuItems.Length + defaults];
            for (int i = 0; i < menuItems.Length; i++)
            {
                _menuItems[i] = menuItems[i];
            }

            // Add defaults.
            if (!_isSub)
            {
                int count = 0;
                _menuItems[menuItems.Length + count] = new MenuItem("Clear", () =>
                  {
                      Console.Clear();
                      this.Show();
                  });
                count++;

                if (_parent != null)
                {
                    _menuItems[menuItems.Length + count] = new MenuItem("Back", () =>
                  {
                      Console.Clear();
                      _parent.Show();
                  });
                    count++;
                }

                _menuItems[menuItems.Length + count] = new MenuItem("Exit", () =>
                {
                    System.Environment.Exit(0);
                });
            }
        }

        /**
         * <summary>
         * Shows menu.
         * </summary>
         **/
        public void Show()
        {
            Console.WriteLine(getPath() + ":");

            for (int i = 0; i < _menuItems.Length; i++)
            {
                Console.WriteLine("{0}: {1}", i+1, _menuItems[i].Message);
            }

            while (!_isExit)
            {
                ReadChosenItem();
            }
        }

        internal string getPath()
        {
            if(_parent == null || _isSub)
            {
                return name;
            }
            else
            {
                return _parent.getPath() + " -> " + name;
            }
        }

        /**
         * <summary>
         * Reads user input and what item they choose.
         * </summary>
         **/
        private void ReadChosenItem()
        {
            if (_menuItems.Length > 0)
            {

                int chosenOne;
                while (true)
                {
                    chosenOne = ConsoleUtils.ReadInt("Choose a menu item: ") - 1;
                    if (chosenOne < _menuItems.Length && chosenOne >= 0)
                    {
                        break;
                    }
                    Console.WriteLine("The chosen item has to be between 1 and {0}", _menuItems.Length);
                }

                _menuItems[chosenOne].Run();
            }
            else
            {
                Console.WriteLine("The menu is empty!");
            }
        }

        /**
         * <summary>
         * Exit menu.
         * </summary>
         **/
        public void Exit()
        {
            _isExit = true;
        }
    }
}
