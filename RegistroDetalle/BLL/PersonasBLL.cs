using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegistroDetalle.Entidades;
using RegistroDetalle.DAL;
using System.Data.Entity;
using System.Linq.Expressions;

namespace RegistroDetalle.BLL
{
    public class PersonasBLL
    {
        public static bool Guardar (Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                if (db.Personas.Add(personas) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Modificar(Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var Anterior = db.Personas.Find(personas.PersonaID);
                foreach (var item in Anterior.Telefonos)
                {
                    if (!personas.Telefonos.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;
                }
                db.Entry(personas).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            { throw; }
            finally
                { db.Dispose(); }

            return paso;
        }
        
        public static bool Eliminar(int Id)
        {
            bool paso = false;
            Contexto db = new Contexto();
            try
            {
                var Eliminar = db.Personas.Find(Id);
                    db.Entry(Eliminar).State = System.Data.Entity.EntityState.Deleted;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Personas Buscar (int Id)
        {
            Contexto db = new Contexto();
            Personas persona = new Personas();
            try
            {
                persona = db.Personas.Find(Id);
                persona.Telefonos.Count();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return persona;
        }


        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona)
        {
            List<Personas> Lista = new List<Personas>();
            Contexto db = new Contexto();
            try
            {
                Lista = db.Personas.Where(persona).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return Lista;
        }

    }
}
