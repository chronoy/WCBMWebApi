using Microsoft.AspNetCore.Mvc;
using Services;

namespace CBMWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentService _equipmentService;
        public EquipmentController(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }
    }
}
