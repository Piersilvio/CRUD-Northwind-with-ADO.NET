using DaoDbNorthwind.contract.enities;

namespace DaoDbNorthwind.DTO
{
    public class ProductsDTO
    {
        public int _productid { get; set; }
        public string _productname { get; set; }
        public int _supplierid { get; set; }
        public int _categoryid { get; set; }
        public string _quantityperunit { get; set; }
        public decimal _unitprice { get; set; }
        public Int16 _unitsinstock { get; set; }
        public Int16 _unitsinorder { get; set; }
        public Int16 _reorderlevel { get; set; }
        public Boolean _discontinued { get; set; }

        //nav. prop.
        public Supliers supliers { get; set; }
        public Categories categories { get; set; }
    }
}
