using BiteBlogs.Models;
using BiteBlogs.Models.NewFolder;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BiteBlogs.Repositories
{


    public class TagRepositoryClass : ITagRepository
    {
        private readonly BiteBlogDbContext biteBlogDbContextObj;

        public TagRepositoryClass(BiteBlogDbContext biteBlogDbContextObj)
        {
            this.biteBlogDbContextObj = biteBlogDbContextObj;
        }


        public async Task<Tag> AddAsync(Tag tag)
        {
            await biteBlogDbContextObj.Tags.AddAsync(tag);
            await biteBlogDbContextObj.SaveChangesAsync();
            return tag;
        }

        public async Task<int> CountAsync()
        {
            var count = await biteBlogDbContextObj.Tags.CountAsync();   
            return count;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await biteBlogDbContextObj.Tags.FindAsync(id);
            if (existingTag != null)
            {
                biteBlogDbContextObj.Tags.Remove(existingTag);
                await biteBlogDbContextObj.SaveChangesAsync();
                return existingTag;
            }
            return null;


        }

        public async Task<IEnumerable<Tag>> GetAllAsync(
            string? searchQuery,
            string? sortBy,
            string? sortDirection,
            int pageSize=100,
            int pageNumber=1
           

            )
        {
            var QueriableTags = biteBlogDbContextObj.Tags.AsQueryable();

            //filtering

            if (string.IsNullOrWhiteSpace(searchQuery) == false)
            {
                QueriableTags = QueriableTags.Where(x => x.Name.Contains(searchQuery) ||
                                                         x.DisplayName.Contains(searchQuery)
                );
            }





            //sorting

            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                var isDesc = string.Equals(sortDirection, "Desc", StringComparison.OrdinalIgnoreCase);

                if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    QueriableTags = isDesc ? QueriableTags.OrderByDescending(x => x.Name) : QueriableTags.OrderBy(x => x.Name);
                }

                if (string.Equals(sortBy, "DisplayName", StringComparison.OrdinalIgnoreCase))
                {
                    QueriableTags = isDesc ? QueriableTags.OrderByDescending(x => x.DisplayName) : QueriableTags.OrderBy(x => x.DisplayName);
                }
            }










            //pagination

            // skip 0 records-take 3 when  pageno=1 (pageno-1)*pagesize
            // skip 3 records-take 3 when pageno=2  (2-1)*3

            var skipRecord = (pageNumber - 1) * pageSize;
            QueriableTags = QueriableTags.Skip(skipRecord).Take(pageSize);

            //------------





            return await QueriableTags.ToListAsync();

          //return  await biteBlogDbContextObj.Tags.ToListAsync();
        }







        public async Task<Tag?> GetAsync(Guid id)
        {
            return await biteBlogDbContextObj.Tags.FirstOrDefaultAsync(t => t.Id == id);
            
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await biteBlogDbContextObj.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await biteBlogDbContextObj.SaveChangesAsync();

                return existingTag;
            }




            return null;
            
        }
    }
}
