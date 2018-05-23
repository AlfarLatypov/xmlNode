using ADMIN.LIB.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


/*Administrator
1.	Должен иметь возможность добавлять новых Поставщиков мобильной связи.
Для добавления поставщика услуг необходимо добавить следующие поля:
- Префиксы номеров телефонов
- Логотип компании
- Наименование сотового оператора
- Процент за обслуживание
Должны быть следующие проверки:
- Если уже добавлен такой поставщик, то система должна сообщить об этом
- Если данный префикс уже используется у другого поставщика услуг – система должна сообщить об этом

*/

namespace ADMIN.LIB
{
    class ServiceProvider
    {

        List<Provider> providers = new List<Provider>();

        List<int> ProvidersPrefix = new List<int>();

        public void addProvider()
        {
            Provider prov = new Provider();
            Console.Write("Введите Название компании : ");
            prov.NameCompany = Console.ReadLine();

            Console.Write("Введите Лого компании : ");
            prov.LogoURL = Console.ReadLine();

            Console.Write("Введите % компании : ");
            prov.Percent = Double.Parse(Console.ReadLine());

            Console.Write("Введите префикс компании : ");

            bool exit = true;
            int pref = 0;

            do
            {
                // prov.Prefix(Int32.....);
                exit = Int32.TryParse(Console.ReadLine(), out pref);
                if (exit && isExistsPrefix(pref))
                    prov.Prefix.Add(pref);

            } while (exit);


            if (isExistsProvider(prov))
            {
                providers.Add(prov);
                ProvidersPrefix.AddRange(prov.Prefix);

            }

            





        }






        private bool isExistsProvider(Provider pro)
        {
            if (providers.Where(w => w.NameCompany == pro.NameCompany).Count() > 0)
            {
                Console.WriteLine("Такой провайдер уже есть!");
                return false;
            }
            return true;
        }


        private bool isExistsPrefix(int pref)
        {
            if (ProvidersPrefix.Where(item => item == pref).Count() > 0) {
                Console.WriteLine("Такой префикс существует");
                return false; }
                return true;
         }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     
        /*Dobavit v XML file*/


        private void AddProviderToXML (Provider prov)
        {
            //проверка должна быть
            XmlDocument doc = new XmlDocument();


            XmlElement elem = doc.CreateElement("Provider");
            XmlElement LogoURL = doc.CreateElement("LogoURL");
            LogoURL.InnerText = prov.LogoURL;
            XmlElement NameCompany = doc.CreateElement("NameCompany");
            NameCompany.InnerText = prov.NameCompany;
            XmlElement Percent = doc.CreateElement("Percent");
            Percent.InnerText = prov.Percent.ToString();

            XmlElement Prefixs = doc.CreateElement("Prefixs");
            foreach (int item in prov.Prefix)
            {
                XmlElement Prefix = doc.CreateElement("Prefix");
                Prefix.InnerText = item.ToString();
                Prefixs.AppendChild(Prefix);
            }


            elem.AppendChild(LogoURL);
            elem.AppendChild(NameCompany);
            elem.AppendChild(Percent);
            elem.AppendChild(Prefixs);
            doc.AppendChild(elem);
            doc.Save("Providers.xml");


        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     

    }
}



           
