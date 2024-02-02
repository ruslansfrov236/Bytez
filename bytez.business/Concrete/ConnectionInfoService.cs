using bytez.business.Abstract;
using bytez.business.Dto.ConnectionInfo;
using bytez.data.Abstract;
using bytez.entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bytez.business.Concrete
{
    public class ConnectionInfoService : IConnectionInfoService
    {
        readonly private IConnectionInfoReadReposItory _connectionInfoReadReposItory;
        readonly private IConnectionInfoWriteReposItory _connectionInfoWriteReposItory;
        readonly private IProductImageService _productImageService;

        public ConnectionInfoService(IProductImageService productImageService, IConnectionInfoWriteReposItory connectionInfoWriteReposItory, IConnectionInfoReadReposItory connectionInfoReadReposItory)
        {
            _connectionInfoReadReposItory = connectionInfoReadReposItory;
            _connectionInfoWriteReposItory = connectionInfoWriteReposItory;
            _productImageService = productImageService;
        }
        public async Task<bool> Create(CreateConnectionInfoDto model)
        {
            _productImageService.IsImage(model.File);
            _productImageService.CheckSize(model.File, 250);

            var newFile = await _productImageService.UploadAsync(model.File);
            await _connectionInfoWriteReposItory.AddAsync(new ConnectionInfo() { Title = model.Title, Description = model.Description, FilePath = newFile });
            await _connectionInfoWriteReposItory.SaveAsync();
            return true;

        }

        public async Task<bool> Delete(string id)
        {
            var connectionInfo = await _connectionInfoReadReposItory.GetByIdAsync(id);
            if (connectionInfo == null) return false;
            var extention = "\\wwwroot\\ui\\assets\\image\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extention, connectionInfo.FilePath);
            _productImageService.Delete(path);
            _connectionInfoWriteReposItory.Remove(connectionInfo);
            await _connectionInfoWriteReposItory.SaveAsync();
            return true;
        }

        public async Task<List<ConnectionInfo>> GetAllConnections()
        {
            List<ConnectionInfo> connectionInfos = await _connectionInfoReadReposItory.GetAll().ToListAsync();

            return connectionInfos;
        }

        public async Task<ConnectionInfo> GetConnectionInfoById(string id)
        => await _connectionInfoReadReposItory.GetByIdAsync(id);

        public async Task<bool> Update(UpdateConnectionInfoDto model)
        {
            var connectionInfo = await _connectionInfoReadReposItory.GetByIdAsync(model.id);
            var extention = "\\wwwroot\\ui\\assets\\image\\";
            var path = Path.Combine(Directory.GetCurrentDirectory(), extention, connectionInfo.FilePath);
            _productImageService.Delete(path);
            if (model.File != null)
            {
                _productImageService.IsImage(model.File);
                _productImageService.CheckSize(model.File, 250);
                var newFile = await _productImageService.UploadAsync(model.File);
                connectionInfo.FilePath = newFile;
            }


            connectionInfo.Title = model.Title;
            connectionInfo.Description = model.Description;
            _connectionInfoWriteReposItory.Update(connectionInfo);
            await _connectionInfoWriteReposItory.SaveAsync();
            return true;
        }
    }
}
