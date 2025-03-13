using Microsoft.AspNetCore.Mvc;
using ServiceLayer.UserServices.Concrete;

namespace HanterxHanterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        // Конструктор контроллера
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Создает нового пользователя на основе данных из ProfileDto.
        /// </summary>
        /// <param name="newProfile">DTO объект с данными для нового пользователя.</param>
        /// <returns>Созданный пользователь.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromForm] UserDto newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid Profile data.");
            }

            try
            {
                var user = await _userService.CreateUserAsync(newUser);
                Console.WriteLine(user.UserId);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновляет существующего пользователя на основе данных из ProfileDto.
        /// </summary>
        /// <param name="newProfile">DTO объект с новыми данными для пользователя.</param>
        /// <returns>Обновленный пользователь.</returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromForm] UserDto newUser)
        {
            if (newUser == null)
            {
                return BadRequest("Invalid Profile data.");
            }

            try
            {
                var user = await _userService.UpdateUserAsync(newUser);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Получить пользователя по идентификатору (для проверки).
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Данные пользователя.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                return BadRequest("Invalid user id.");
            }

            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
