using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Messages
{
    public class Message
    {
        public static class User
        {
            public static string Update()
            {
                return "kullanıcı güncellendi";
            }

            public static string Delete()
            {
                return "kullanıcı silindi";
            }

            public static string WrongPassword()
            {
                return "şifre hatalı";
            }

            public static string ChangedPassword()
            {
                return "şifre değiştirildi";
            }
        }
        public static class OperationClaim
        {
            public static string Add()
            {
                return "yetkilendirme oluşturuldu";
            }

            public static string Update()
            {
                return "yetkilendirme güncellendi";
            }

            public static string Delete()
            {
                return "yetkilendirme silindi";
            }

            public static string NameIsNotAvaliable()
            {
                return "bu yetki adı daha önce kullanılmış";
            }
        }
        public static class UserOperationClaim
        {
            public static string Add()
            {
                return "yetkilendirme başarılı";
            }

            public static string Updata()
            {
                return "yetkilendirme Güncellendi";
            }

            public static string Delete()
            {
                return "yetkilendirme Silindi";
            }

            public static string SetAvaliable()
            {
                return "bu kullanıcıya bu yetki daha önce atanmış";
            }

            public static string OperationClaimNotExist()
            {
                return "seçtiğiniz yetki bilgisi yetkilerde bulunmuyor";
            }

            public static string UserNotExist()
            {
                return "seçtiğiniz kullanıcı bilgisi yetkilerde bulunmuyor";
            }
        }
    }
}
