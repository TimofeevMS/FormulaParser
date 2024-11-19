using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Parser.Application.Common;
using Parser.Application.UseCases.DataSheets.GetTemplates;
using Parser.Domain.Entities;
using Parser.Domain.Interfaces;

namespace Parser.Application.Tests.UseCases.DataSheets.GetTemplates;

[TestClass]
[TestSubject(typeof(GetTemplatesHandler))]
public class GetTemplatesHandlerTest
{
    private readonly Guid _id = Guid.NewGuid();
    
    private Mock<IUnitOfWork> _mockUnitOfWork;
    private Mock<IMapper> _mockMapper;
    private Mock<ITemplateEfCoreRepository> _mockRepository;
    private GetTemplatesHandler _handler;
    private Template _template;
    
    [TestInitialize]
    public void Setup()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMapper = new Mock<IMapper>();
        _mockRepository = new Mock<ITemplateEfCoreRepository>();

        _mockUnitOfWork.Setup(u => u.GetRepository<ITemplateEfCoreRepository>())
                       .Returns(_mockRepository.Object);

        _template = new Template { Id = _id, Name = "Test Template", Attributes = new List<TemplateAttribute>() };
        _mockRepository.Setup(r => r.GetByIdAsync(_id, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(_template);
        
        _mockMapper.Setup(m => m.Map<List<GetForDataSheetValue>>(_template.Attributes))
                   .Returns(new List<GetForDataSheetValue>());

        _handler = new GetTemplatesHandler(_mockUnitOfWork.Object, _mockMapper.Object);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnTemplate_WhenTemplateExists()
    {
        // Arrange
        var request = new GetTemplatesRequest(_id);
        

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_template.Id, result.Data?.Id);
        Assert.AreEqual(_template.Name, result.Data?.Name);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnError_WhenTemplateDoesNotExist()
    {
        // Arrange
        var request = new GetTemplatesRequest(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(!result.Succeeded);
        Assert.AreEqual(Errors.Template.NotFound, result.Errors.First());
    }

    [TestMethod]
    public async Task Handle_ShouldReturnError_WhenIdIsNull()
    {
        // Arrange
        var request = new GetTemplatesRequest(null);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(!result.Succeeded);
        Assert.AreEqual(Errors.Template.Identifier, result.Errors.First());
    }
}