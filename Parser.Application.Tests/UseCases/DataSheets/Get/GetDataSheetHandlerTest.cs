using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Parser.Application.Common;
using Parser.Application.UseCases.DataSheets.Get;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.Tests.UseCases.DataSheets.Get;

[TestClass]
[TestSubject(typeof(GetDataSheetHandler))]
public class GetDataSheetHandlerTest
{
    private readonly Guid _id = Guid.NewGuid();
    
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IDataSheetEfCoreRepository> _mockRepository;
    private Mock<IMapper> _mockMapper;
    private GetDataSheetHandler _handler;
    private DataSheet _dataSheet;

    [TestInitialize]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _mockRepository = new Mock<IDataSheetEfCoreRepository>();

        _mockUnitOfWork.Setup(u => u.GetRepository<IDataSheetEfCoreRepository>())
                       .Returns(_mockRepository.Object);

        _dataSheet = new DataSheet { Id = _id, Name = "Test" };

        _mockRepository.Setup(r => r.GetByIdAsync(_id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(_dataSheet);

        _mockMapper.Setup(m => m.Map<GetDataSheetResponse>(_dataSheet))
                   .Returns(new GetDataSheetResponse { Id = _id, Name = "Test" });

        _handler = new GetDataSheetHandler(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnDataSheet_WhenDataSheetExists()
    {
        // Arrange
        var request = new GetDataSheetRequest(_id);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_dataSheet.Id, result.Data?.Id);
        Assert.AreEqual(_dataSheet.Name, result.Data?.Name);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnError_WhenDataSheetDoesNotExist()
    {
        // Arrange
        var request = new GetDataSheetRequest(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(!result.Succeeded);
        Assert.AreEqual(Errors.DataSheet.NotFound, result.Errors.First());
    }
}