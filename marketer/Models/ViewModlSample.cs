
using System;
using System.ComponentModel.DataAnnotations;
///////  کلاسی برای چک کردن کد ملی به عنوان دیتا انوتیشن
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NationalCodeType : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string value1 = "";
            if (value is string)
            {
                value1 = value as string;
            }
            else
            {
                return false;
            }
            if (value1 == null) return false;
            string vID = value1.Trim();

            long _s = 0;
            long _r = 0;
            if (!Int64.TryParse(vID, out _r))
                return false;
            if (vID.Length <= 9 || vID.Length > 11)
                return false;

            int[] digits = new int[vID.Length];
            for (int i = 0; i < vID.Length; i++)
            {
                digits[i] = Convert.ToInt32(vID.Substring(i, 1));
            }
            if (vID.Length == 11)
            {
                int[] p = { 29, 27, 23, 19, 17, 29, 27, 23, 19, 17 };
                for (int i = 0; i < 10; i++)
                {
                    _s = _s + (digits[i] + digits[9] + 2) * p[i];
                }
                System.Math.DivRem(_s, 11, out _r);
                if (_r == 10)
                    _r = 0;
                if (_r != digits[10])
                    return false;
            }
            if (vID.Length == 10)
            {

                if (vID == "0000000000" || vID == "2222222222" || vID == "3333333333" || vID == "4444444444"
                    || vID == "5555555555" || vID == "6666666666" || vID == "7777777777" || vID == "8888888888" || vID == "9999999999")
                    return false;

                for (int i = 0; i < 9; i++)
                {
                    _s = _s + (digits[i]) * (10 - i);
                }
                _r = _s - (int)Math.Floor(Convert.ToDecimal(_s) / 11) * 11;
                if (!((_r == 0 && _r == digits[9]) || (_r == 1 && digits[9] == 1) || (_r > 1 && digits[9] == 11 - _r)))
                    return false;
            }
            return true;
        }
    }
///////  کلاسی برای چک کردن کد ملی به عنوان دیتا انوتیشن





//////////  Class برای الگو ها و ریگولار اکسپرشن ها
    public static class RegexPatten
    {
        public const string PersianDate = @"^[1-4]\d{3}\/((0?[1-6]\/((3[0-1])|([1-2][0-9])|(0?[1-9])))|((1[0-2]|(0?[7-9]))\/(30|([1-2][0-9])|(0?[1-9]))))$";
        public const string PersianDateTime = @"^(13|14)\d\d/(0[1-9]|1[012])/(0[1-9]|[12][0-9]|3[01]) \d\d:\d\d:\d\d$";

        public const string TellNumber = @"^0[0-9]{2,}[0-9]{7,}$";

        public const string OnlyEnglishText = @"[A-Za-z0-9-]+";

        public const string OnlyPersianText = @"[\u0600-\u06FF0-9-]+";

        public const string OnlyPersianTextWithSpace = @"[\u0600-\u06FF 0-9-]+";

        public const string Ip = @"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";

        public const string CellPhone = @"^09[0-9][0-9]{8}$";

        public const string Image = @"^.*\.(jpg|gif|png|jpeg)$";

        public const string CreditCart = @"^[0-9]{16}$";

        public const string CreditCartWithDash = @"^[0-9]{4}\-[0-9]{4}\-[0-9]{4}\-[0-9]{4}$";

        public const string PostCode = @"^[0-9]{10,}$";

        public const string OnlyNumber = @"^-?\d+$";

        public const string Url = @"^http(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\=\,\'\/\\\+&amp;%\$#_]*)?$";

        public const string ImageUrl = @"(?:([^:/?#]+):)?(?://([^/?#]*))?([^?#]*\.(?:jpg|gif|png)";

        public const string VideoUrl = @"(?:([^:/?#]+):)?(?://([^/?#]*))?([^?#]*\.(?:flv|mp4))(?:\?([^#]*))?(?:#(.*))?";

        public const string JpgUrl = @"(?:([^:/?#]+):)?(?://([^/?#]*))?([^?#]*\.(?:jpg|jpeg))(?:\?([^#]*))?(?:#(.*))?";

        public const string PasswordOneNumberOneLetter7To20 = @"((?=.*\d)(?=.*[a-z]).{7,20})";

        public const string CammaSeperateInteger = @"^(\d+(,\d+)*)?$";
    }
//////////  Class برای الگو ها و ریگولار اکسپرشن ها


//////  کلاس ویو مدل
public class CustomerCreateViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام الزامیست")]
        public string Name { get; set; }
        [Required(ErrorMessage = "وارد کردن حداقل یک آدرس الزامیست")]
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string SerialNo { get; set; }
        [RegularExpression(RegexPatten.TellNumber,ErrorMessage ="02188445566 شماره تلفن صحیح نیست")]
        public string Tel { get; set; }
        [RegularExpression(RegexPatten.CellPhone,ErrorMessage ="شماره موبایل صحیح 09122500258")]
        public string Mob { get; set; }
        [EmailAddress(ErrorMessage ="ایمیل صحیح نیست text@example.com")]
        public string Email { get; set; }
        public int? AccCode { get; set; }
        public decimal? Credit { get; set; }
        public string Moaref { get; set; }
        [RegularExpression(RegexPatten.PersianDate,ErrorMessage ="تاریخ ثبت صحیح نیست")]
        public string RegDate { get; set; }
        public string RegTime { get; set; }
        public string UserCodeReg { get; set; }
        public int ShareAmount { get; set; }
        public byte? ShareType { get; set; }
        public int? CityCode { get; set; }
        public int? StateCode { get; set; }
        public string PassWord { get; set; }
        public decimal Debit { get; set; }
        public decimal DebitOil { get; set; }
        public decimal PayMent { get; set; }
        public byte CustType { get; set; }
        public short? Percent { get; set; }
        public int? MoeinId { get; set; }
        public byte Locked { get; set; }
        public byte Grp1 { get; set; }
        public byte Grp2 { get; set; }
        public int? ReceiverCode { get; set; }
        public string AliasName { get; set; }
        public byte Closed { get; set; }
        public int AccMcode { get; set; }
        public int CustomerClass { get; set; }
        public string ZipCode { get; set; }
        [NationalCodeType(ErrorMessage ="شماره ملی صحیح نیست لطفا شماره ملی صحیح وارد کنید")]
        public string IdCode { get; set; }
        public string RegCode { get; set; }
        public string EcoCode { get; set; }
        public bool IsCompany { get; set; }
        public int Version { get; set; }
        public int PriceGroup { get; set; }
        public bool Archive { get; set; }
        public int ILoc { get; set; }
        public byte Oprtype { get; set; }
        public bool? NotificationSms { get; set; }
        public bool IsShipmentOwner { get; set; }
    }

//////  کلاس ویو مدل