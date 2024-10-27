using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using myNotesService.Controllers;
using myNotesService.Model;
using myNotesService.Services;
using myNotesService.TestUtils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace myTodoService.Tests.Controllers
{
    public class GeneralNotesControllerTests
    {

        private readonly Mock<IGeneralNotesService> _mockService;
        private readonly GeneneralNotesController _controller;
        private readonly NotesTestUtils _testUtils;

        public GeneralNotesControllerTests()
        {
            // Arrange
            _mockService = new Mock<IGeneralNotesService>();
            _controller = new GeneneralNotesController(_mockService.Object);
            _testUtils = new();
        }
        [Fact]
        public void GetAllGeneralNotes_returnsOk_andObject()
        {
            // Given
            IEnumerable<GeneralNotes> notes = _testUtils.CreateMultipleGeneralNotes();
            _mockService.Setup(service => service.GetAllGeneralNotes()).Returns(notes);
            // When
            var result = _controller.GetAllGeneralNotes();
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTasks = Assert.IsAssignableFrom<IEnumerable<GeneralNotes>>(okResult.Value);
            Assert.Equal(_testUtils.GetNumberOfNotes(), returnedTasks?.Count());

        }
        [Fact]
        public void GetAllGeneralNotes_retrunsNotFound()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetGeneralNotesById_returnOK_andObject()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetGeneralNotesById_returnNotFound()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByKeyword_returnOK_and_Object()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByKeyword_returnNotFound_keywordsEmptyOrNull()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByKeyword_returnNotFound_keywordsNotFound()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByOwner_returnsOk_andObject()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByOwner_returnsNotFond_ownerEmptyOrNull()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByOwner_returnsNotFond_NotesNotFound()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByDateCreated_returnsOk_andObject()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void GetNotesByDateCreated_returnsNotFond()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void UpdateGeneralNotes_returnOK_andObject()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void UpdateGeneralNotes_returnNotFound_NotesNull()
        {
            // Given

            // When

            // Then
        }
        [Fact]
        public void DeleteGenaralNotes_returnNotFound()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteGeneralNote(id)).Returns(false);

            // When
            var result = _controller.DeleteGenaralNotes(id);

            // Then
            Assert.IsType<NotFoundResult>(result);

        }
        [Fact]
        public void DeleteGenaralNotes_resturnNoContent()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteGeneralNote(id)).Returns(true);

            // When
            var result = _controller.DeleteGenaralNotes(id);

            // Then
            Assert.IsType<NoContent>(result);


        }
    }
}
