using System;
namespace YuvamHazir.API.DTOs
{
	public class UserDto
	{
        // Kullanıcı listeleme
        public class UserListDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
        }

        // Kullanıcı detay (profil)
        public class UserDetailDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int TotalPatiPoints { get; set; }
        }

        // Kayıt için gelen veri
        public class UserRegisterDto
        {
            public string FullName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public string Password { get; set; }
        }

        // Giriş için gelen veri
        public class UserLoginDto
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        // Giriş response (token vs.)
        public class UserLoginResponseDto
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string Role { get; set; }
            public string Token { get; set; }
        }

    }
}

