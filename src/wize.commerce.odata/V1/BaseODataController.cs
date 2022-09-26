using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using wize.commerce.data;
using wize.common.tenancy.Interfaces;

namespace wize.commerce.odata.V1
{
    [ApiVersion("1.0")]
    [ODataRoutePrefix("[controller]")]
    [Authorize]
    public abstract class BaseODataController<TKey, TModel> : ODataController where TModel : class
    {
        private readonly WizeContext _context;
        private readonly ITenantProvider _tenantProvider;
        private readonly ILogger<BaseODataController<TKey, TModel>> _logger;

        public BaseODataController(ILogger<BaseODataController<TKey, TModel>> logger, IActionDescriptorCollectionProvider actionProvider, WizeContext context, ITenantProvider tenantProvider)
        {
            _logger = logger;
            _context = context;
            _tenantProvider = tenantProvider;
        }

        /// <summary>
        /// OData based GET operation.
        /// This method will return the requested Dataset.
        /// </summary>
        /// <returns>IQueryable of requested type.</returns>
        [ODataRoute]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)]
        public virtual ActionResult<IQueryable<TModel>> Get()
        {
            try
            {
                Guid? tenantId = _tenantProvider.GetTenantId();
                return Ok(_context.Set<TModel>().Where(a => EF.Property<Guid>(a, "TenantId") == tenantId.Value));
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error: Get():{0}", typeof(TModel).Name);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// OData based GET(id) operation.
        /// This method receives a key value and will return the respective record if it exists.
        /// </summary>
        /// <param name="id">Key value</param>
        /// <returns>Data model</returns>
        [ODataRoute("({id})")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Get(TKey id)
        {
            try { 
                //_context.Set<TModel>().Single(m => m.)
                Guid? tenantId = _tenantProvider.GetTenantId();
                var model = _context.Find<TModel>(id);

                if (model == null)
                {
                    _logger.LogWarning("Warning: Get(id):{0} NotFound", typeof(TModel).Name);
                    return NotFound();
                }
                return Ok(model);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "Error: Get {0}", typeof(TModel).Name);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// OData based POST operation.
        /// This method receives a model and attempts to insert that record into the appropriate datastore.
        /// </summary>
        /// <param name="model">Data model</param>
        /// <returns>Data model</returns>
        [ODataRoute]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual IActionResult Post([FromBody] TModel model)
        {
            try { 
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                _context.Set<TModel>().Add(model);
                _context.SaveChanges();

                return Created(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: Post():{0}", typeof(TModel).Name);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// OData based PATCH operation.
        /// This method receives a PatchDocument and attempts to apply the specified changes.
        /// </summary>
        /// <param name="id">Key value</param>
        /// <param name="delta">Delta changeset</param>
        /// <returns>Data model</returns>
        /// 
        [HttpPatch]
        [ODataRoute("({id})")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Patch(TKey id, Delta<TModel> delta)
        {
            try { 
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var model = _context.Find<TModel>(id);
                //if (EF.Property<Guid>(model, "TenantId") == _tenantProvider.GetTenantId())
                //    return BadRequest();

                if (model == null)
                    return NotFound();

                delta.Patch(model);
                _context.SaveChanges();

                return Updated(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: Patch(id):{0}", typeof(TModel).Name);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// OData based PUT operation.
        /// This method receives a key value and a data model and attempts to apply the updated model to the existing record.
        /// </summary>
        /// <param name="id">Key value</param>
        /// <param name="model">Data model</param>
        /// <returns>Data model</returns>
        ///
        [HttpPut]
        [ODataRoute("({id})")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Put(TKey id, [FromBody] TModel model)
        {
            try { 
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var origModel = _context.Find<TModel>(id);
                    
                if (origModel == default)
                {
                    return NotFound();
                }
                _context.Entry(origModel).State = EntityState.Detached;
                _context.Attach(model);
                _context.Update<TModel>(model);
                _context.SaveChanges();

                return Updated(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: Put(id):{0}", typeof(TModel).Name);
                return new StatusCodeResult(500);
            }
        }

        /// <summary>
        /// OData based DELETE operation.
        /// This method receives a key value and attempts to delete the appropriate record from the datastore.
        /// </summary>
        /// <param name="id">Key value</param>
        /// <returns></returns>
        [ODataRoute("({id})")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual IActionResult Delete(TKey id)
        {
            try { 
                var model = _context.Find<TModel>(id);
                //if (EF.Property<Guid>(model, "TenantId") == _tenantProvider.GetTenantId())
                //    return BadRequest();

                if (model == null)
                    return NotFound();

                _context.Remove<TModel>(model);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: Delete():{0}", typeof(TModel).Name);
                return new StatusCodeResult(500);
            }
        }
    }
}