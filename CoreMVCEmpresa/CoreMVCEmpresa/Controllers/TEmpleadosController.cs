using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreMVCEmpresa.Models.Context;
using CoreMVCEmpresa.Models.Entities;
using Microsoft.Data.SqlClient;




namespace CoreMVCEmpresa.Controllers
{
    public class TEmpleadosController : Controller
    {
        private readonly DBContext _context;

        public TEmpleadosController(DBContext context)
        {
            _context = context;
        }

        // GET: TEmpleados
        public async Task<IActionResult> Index()
        {
			ViewBag.IdPuesto = new SelectList(_context.TCatPuesto.Select(p => new SelectListItem
			{
				Value = p.IdPuesto.ToString(),
				Text = p.NombrePuesto
			}), "Value", "Text");
			var dBContext = _context.TEmpleados.Include(t => t.IdPuestoNavigation);
            return View(await dBContext.ToListAsync());
        }

        // GET: TEmpleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TEmpleados == null)
            {
                return NotFound();
            }

            var tEmpleados = await _context.TEmpleados
                .Include(t => t.IdPuestoNavigation)
                .FirstOrDefaultAsync(m => m.IdNumEmp == id);
            if (tEmpleados == null)
            {
                return NotFound();
            }

            return View(tEmpleados);
        }

        // GET: TEmpleados/Create
        public IActionResult Create()
        {
            ViewBag.IdPuesto = new SelectList(_context.TCatPuesto.Select(p => new SelectListItem
            {
                Value = p.IdPuesto.ToString(),
                Text = p.NombrePuesto
            }), "Value", "Text");

            return View();
        }

        // POST: TEmpleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdNumEmp,Nombre,Apellidos,Activo,IdPuesto")] TEmpleados tEmpleados)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tEmpleados);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPuesto"] = new SelectList(_context.TCatPuesto, "IdPuesto", "IdPuesto", tEmpleados.IdPuesto);
            return View(tEmpleados);
        }

        // GET: TEmpleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TEmpleados == null)
            {
                return NotFound();
            }

            var tEmpleados = await _context.TEmpleados.FindAsync(id);
            if (tEmpleados == null)
            {
                return NotFound();
            }
            ViewBag.IdPuesto = new SelectList(_context.TCatPuesto.Select(p => new SelectListItem
            {
                Value = p.IdPuesto.ToString(),
                Text = p.NombrePuesto
            }), "Value", "Text");
            return View(tEmpleados);
        }

        // POST: TEmpleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdNumEmp,Nombre,Apellidos,Activo,IdPuesto")] TEmpleados tEmpleados)
        {
            if (id != tEmpleados.IdNumEmp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tEmpleados);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TEmpleadosExists(tEmpleados.IdNumEmp))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPuesto"] = new SelectList(_context.TCatPuesto, "IdPuesto", "IdPuesto", tEmpleados.IdPuesto);
            return View(tEmpleados);
        }

        // GET: TEmpleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TEmpleados == null)
            {
                return NotFound();
            }

            var tEmpleados = await _context.TEmpleados
                .Include(t => t.IdPuestoNavigation)
                .FirstOrDefaultAsync(m => m.IdNumEmp == id);
            if (tEmpleados == null)
            {
                return NotFound();
            }

            return View(tEmpleados);
        }

        // POST: TEmpleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TEmpleados == null)
            {
                return Problem("Entity set 'DBContext.TEmpleados'  is null.");
            }
            var tEmpleados = await _context.TEmpleados.FindAsync(id);
            if (tEmpleados != null)
            {
                _context.TEmpleados.Remove(tEmpleados);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TEmpleadosExists(int id)
        {
          return (_context.TEmpleados?.Any(e => e.IdNumEmp == id)).GetValueOrDefault();
        }
		//==================================AJAX=========================================



		[HttpPost]
		public IActionResult DeleteEmpleado(int id)
		{
			try
			{
				var Empleado = _context.TEmpleados.Find(id);
				if (Empleado == null)
				{
					return NotFound();
				}
				_context.Database.ExecuteSqlRaw("EXEC SP_ELIMINAR_EMPLEADO {0}", id);
				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpPost]
		public IActionResult InsertEmpleado(string nombre, string apellidos, int idPuesto)
		{

			var empleado = new TEmpleados
			{
				Nombre = nombre,
				Apellidos = apellidos,
				IdPuesto = idPuesto
			};

			_context.Database.ExecuteSqlRaw("EXEC SP_INSERTAR_EMPLEADO @Nombre={0}, @Apellidos={1}, @Id_Puesto={2}", nombre, apellidos, idPuesto);

			return Ok();
		}





	}
}
