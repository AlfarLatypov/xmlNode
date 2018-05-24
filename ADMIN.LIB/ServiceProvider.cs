using ADMIN.LIB.Module;
using System;
using System.Collections.Generic;
using System.IO;
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
   public class ServiceProvider
    {

        public ServiceProvider() : this("") { }
        public ServiceProvider(string path)
        {
            if (string.IsNullOrEmpty(path))
      this.path = Path.Combine(@"\\dc\Студенты\ПКО\SEB-171.2\C#", "Operators.xml");
            else this.path = path;
        }



        List<Provider> providers = new List<Provider>();

        List<int> ProvidersPrefix = new List<int>();

        private string path { get; set; }

      


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
                AddProviderToXML(prov);

            }
            
        }

        public void EditProvider()
        {
            Console.Write("Введите имя провайдера - ");
            SearchProviderByNameForEdit(Console.ReadLine());
                        
        }



        //-----------------------------------------------------//
        public void DeleteProvider() { }



        //-----------------------------------------------------//
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
            XmlDocument doc = getDocument();


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
            doc.DocumentElement.AppendChild(elem);
            
            doc.Save(path);


        }


         public XmlDocument getDocument()
        {
            XmlDocument xd = new XmlDocument();

            
            FileInfo fi = new FileInfo(path);

            if (fi.Exists)
            {
                xd.Load(path);
            }
            else
            {
                //1
                //FileStream fs = fi.Create();
                //fs.Close();

                //2
                XmlElement xl = xd.CreateElement("Providers");
                xd.AppendChild(xl);
                xd.Save(path);
            }
            return xd;

        }

        public void SearchProviderByNameForEdit(string name)
        {

            XmlDocument xd = getDocument();
            XmlElement root = xd.DocumentElement;

            //1
            bool find = false;
            foreach (XmlElement item in root)
            {
                find = false;

                foreach (XmlNode i in item.ChildNodes)
                {
                    if (i.Name == "NameCompany" && i.InnerText == name)
                        find = true;
                }
                if (find)
                {
                    XmlElement el = Edit(item);
                    break;
                }
            }
            if (find)
                xd.Save(path);
        
            //2 
            //  Console.WriteLine(xn.SelectSingleNode("NameCompany").InnerText);

        }

        private XmlElement Edit ( XmlElement prov)
        {
            foreach (XmlElement item in prov.ChildNodes)
            {
                Console.WriteLine(item.Name + ": (" + item.InnerText + ") - ");
                string cn = Console.ReadLine();
                if (!string.IsNullOrEmpty(cn))
                    item.InnerText = cn;
            }
            return prov;
        }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////     

    }
}



           
