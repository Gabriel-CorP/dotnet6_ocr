namespace OCR.BL
{
    public class Validaciones
    {
        public Validaciones()
        {
            
        }
        public bool DeleteImage(string ruta)
        {
            try
            {
                if (IsOccupaded(ruta))
                {
                    try
                    {
                        System.IO.File.Delete(ruta);
                    }
                    catch (Exception e)
                    {

                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public bool IsOccupaded(string ruta)
        {
            if (System.IO.File.Exists(ruta))
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public string Fecha()
        {
            return DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        }
    }
}
