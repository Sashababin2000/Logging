using System.Text;
namespace Logging
{
    public static class Logging
    {
        public static string path; // путь для сохранения логов
        public static Encoding encoding; // кодировка

        public static void Trace(string text) // Очень подробные сообщения журнала, потенциально высокой частоты и объема.
        {
            string message = "[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ff") + "]" + " [TRACE] : " + text;
            Writer(message);
        }

        public static void Debug(string text) // Менее подробные и/или менее частые сообщения об отладке
        {
            string message = "[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ff") + "]" + " [DEBUG] : " + text;
            Writer(message);
        }

        public static void Info(string text) // Информационные сообщения
        {
            string message = "[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ff") + "]" + " [INFO ] : " + text;
            Writer(message);
        }

        public static void Error(Exception exception) // Сообщения об ошибках
        {
            string message = "[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ff") + "]" + " [ERROR] : " + exception + "/n" + exception.StackTrace;
            Writer(message);
        }

        public static void Warning(Exception exception) // Сообщения о предупреждениях.
        {
            string message = "[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ff") + "]" + " [WARN ] : " + exception + "/n" + exception.StackTrace;
            Writer(message);
        }

        public static void Fatal(Exception exception) //  Сообщения о фатальных ошибках. После фатальной ошибки приложение обычно завершается.
        {
            string message = "[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ff") + "]" + " [FATAL] : " + exception + "/n" + exception.StackTrace;
            Writer(message);
        }

        private static void Writer(string message) // Метод для записи логов
        {
            if (path == null) //Если путь пустой, то созддает файл логов в корневой папке приложения.
                path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + DateTime.Now.ToString("dd_MM_yyy") + "_logs.txt";
            if (encoding == null) // Если кодировка пустая, то задаем Кодировку Windows-1251 (Кириллица)
                encoding = Encoding.GetEncoding(1251);

            try
            {
                StreamWriter sw = new StreamWriter(path, true, encoding);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }
            catch (Exception s)
            {
                path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + DateTime.Now.ToString("dd_MM_yyy") + "_LOGGINGV2.txt";
                encoding = Encoding.GetEncoding(1251);
                StreamWriter sw = new StreamWriter(path, true, encoding);
                sw.WriteLine("[" + DateTime.Now.ToString("dd.MM.yy HH:mm:ff") + "]" + " [WARN ] : " + s + "/n" + s.StackTrace);
                sw.WriteLine(message);
                sw.Close();
                sw.Dispose();
            }
        }
    }
}