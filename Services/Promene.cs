namespace Vozila.Services
{
    public static class Promene
    {
        private static string ubaceno = "New record  ++ \r\n";
        private static string obrisano = "Deleted    -- \r\n";
        public static void ZapisiUnos()
        {
            File.AppendAllText("log.txt",ubaceno);
        }
        public static void ZapisiBrisanje()
        {
            File.AppendAllText("log.txt", obrisano);
        }
    }
}
