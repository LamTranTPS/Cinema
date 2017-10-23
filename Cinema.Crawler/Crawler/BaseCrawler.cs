using Cinema.Data.Infrastructure;
using Cinema.Data.Repositories;
using Cinema.Model.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Crawler.Crawler
{
    public class BaseCrawler<T> where T : class
    {
        protected HtmlWeb _htmlWeb;
        protected IErrorRepository _errorRepository;
        protected RepositoryBase<T> _repository;

        public BaseCrawler()
        {
            _htmlWeb = new HtmlWeb()
            {
                AutoDetectEncoding = false,
                OverrideEncoding = Encoding.UTF8
            };
            _errorRepository = new ErrorRepository();
            
        }

        public List<T> CrawlerData()
        {
            return null;
        }

        protected List<T> TryCrawler()
        {
            try
            {
                return CrawlerData();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex, GetType().ToString());
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx, GetType().ToString());
            }
            catch (Exception ex)
            {
                LogError(ex, GetType().ToString());
            }
            return null;
        }

        private void LogError(Exception ex, string action)
        {
            try
            {
                Error error = new Error();
                error.Action = "Data crawler: " + action;
                error.CreatedDate = DateTime.Now;
                error.Message = ex.Message;
                error.StackTrace = ex.StackTrace;
                _errorRepository.Add(error);
            }
            catch
            {
            }
        }
    }
}
