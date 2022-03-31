using System.Runtime.InteropServices;

namespace dektopCS
{
    //Служебный класс для проверки наличия соединения с сетью Интернет
    public class InternetAvailability
    {
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsInternetAvailable()
        {
            return InternetGetConnectedState(out _, 0);
        }
    }
}
