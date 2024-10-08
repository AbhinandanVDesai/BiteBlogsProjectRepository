﻿
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BiteBlogs.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;  
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account=new Account(
                 configuration.GetSection("Cloudinary")["CloudName"],
                 configuration.GetSection("Cloudinary")["ApiKey"],
                 configuration.GetSection("Cloudinary")["ApiSecret"]
                
                
                );
            
            
        }



        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);   //create a cloudinay account first .then upload the paramass

            var uploadParams = new ImageUploadParams()
            {
                File=new FileDescription(file.Name,file.OpenReadStream()),
                DisplayName=file.Name
            };

            var uploadResult= await client.UploadAsync(uploadParams);     //till here code is working fine
            
            if(uploadResult !=null && uploadResult.StatusCode==System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUrl.ToString();
                
            }

            return null;
        }
    }
}
