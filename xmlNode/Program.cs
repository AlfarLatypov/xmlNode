using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ADMIN.LIB;


namespace xmlNode
{

    /*Цель этой системы - предоставить решение для оплаты мобильной связи с использованием единой системы.
     В этой системе Администратор должен иметь возможность добавлять в систему новых поставщиков мобильной связи. 
     Если клиент сталкивается с проблемами в онлайн-платеже или других транзакциях, 
     то клиент должен иметь возможность регистрировать жалобу.
*/




    class Program
    {
        static void Main(string[] args)
        {

            ServiceProvider sp = new ServiceProvider();
            //sp.addProvider();
            sp.EditProvider();

        }

    }
}
