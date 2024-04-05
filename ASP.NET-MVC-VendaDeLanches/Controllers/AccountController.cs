using ASP.NET_MVC_VendaDeLanches.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_MVC_VendaDeLanches.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET do formulário de login
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        // POST do formulário, onde é feita a validação dos dados enviados
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            // Validar se o Modelo nãoi é válido e retornar os erros
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }

            // Se o modelo for válido, seria usado o método FindByNameAsync para encontrar o nome de usuário na tabela do Identity
            var user = await _userManager.FindByNameAsync(loginVM.UserName);

            // Se o usuário não for nulo
            if(user != null)
            {
                // verificaremos se a senha é a correta, e verificar se o cookie de entrada deve persistir (primeiro false), e informar 
                // se o login falhar se a conta deve ser bloqueada (segundo false)
                var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);

                // Se a combinação de usuário e senha for correta	
                if(result.Succeeded)
                {
                    // Se a returnUrl for nulo ou vazia direcionaremos para a página Index.
                    if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    // Se a returnUrl não for nula ou vazia, direcionaremos para a returnUrl informada
                    return View(loginVM.ReturnUrl);
                }
            }

            // Se todas as verificações acima falhar, indica que o usuário não existe.
            // Retornaremos um erro para a ModelState
            ModelState.AddModelError("", "Falha ao realizar o login!!");
            return View(loginVM);
        }

        public IActionResult Register()
        {
            return View();
        }

        // O POST do Register passará as credenciais da LoginViewModel como parâmetro
        // O atributo ValidateAntiForgeryToken evita ataques CSRV (Cross Site Request Forgery)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            // Verificar se o modelo é válido
            if (ModelState.IsValid)
            {
                // Instânciar um IdentityUser passando o nome de usuário obtido da registroVM
                var user = new IdentityUser() { UserName = registroVM.UserName };

                // Atribuir uma variável chamada result, onde informaremos para criar um usuário passando o objeto user e a senha obtida da registroVM
                var result = await _userManager.CreateAsync(user, registroVM.Password);

                // Se o processo ocorrer com sucesso
                if (result.Succeeded)
                {
                    // Retornaremos o usuário para a view Login da action Account
                    return RedirectToAction("Login", "Account");
                }
                // Caso falhe
                else
                {
                    // Retornaremos um erro para a ModelState
                    this.ModelState.AddModelError("Registro", "Falha ao realizar o registro");
                }
            }

            // Retornaremos a view registroVM
            return View(registroVM);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Remove todos os valores (conteúdo) dos objetos na Session. A sessão com a mesma
            // chave continuará viva.
            HttpContext.Session.Clear();

            // Informamos que não há um usuário atribuído
            HttpContext.User = null;

            // O método SignOutAsync() faz o logout do usuário
            await _signInManager.SignOutAsync();

            // Retornaremos o usuário para a página principal
            return RedirectToAction("Index", "Home");
        }
    }
}
