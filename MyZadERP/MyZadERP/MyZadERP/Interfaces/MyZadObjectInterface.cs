using System;
using System.Collections.Generic;
using System.Text;

namespace MyZadERP.Interfaces
{
    public abstract class MyZadObjectInterface
    {
        private int id;

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }
    }
}
