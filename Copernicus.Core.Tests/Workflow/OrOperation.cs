using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Copernicus.Core.Tests.Workflow
{
    public class OrOperation
    {
        [Fact]
        public void Execute()
        {
            Copernicus.Core.Workflow.OrOperation TempOperation = new Core.Workflow.OrOperation();
            Assert.Equal(1, TempOperation.Execute(1));
            Assert.Equal("A", TempOperation.Execute("A"));
        }

        [Fact]
        public void StartFailedOperation()
        {
            Copernicus.Core.Workflow.OrOperation TempOperation = new Core.Workflow.OrOperation();
            TempOperation.SuccessOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => { throw new ArgumentException("A"); } });
            TempOperation.FailureOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => x.Value + 1 });
            Assert.False(TempOperation.Start(1).Result);
        }

        [Fact]
        public void StartMultipleSubOperation()
        {
            Copernicus.Core.Workflow.OrOperation TempOperation = new Core.Workflow.OrOperation();
            TempOperation.SuccessOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => x.Value + 1 });
            TempOperation.SuccessOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => x.Value + 1 });
            Assert.True(TempOperation.Start(1).Result);
        }

        [Fact]
        public void StartNoSubOperations()
        {
            Copernicus.Core.Workflow.OrOperation TempOperation = new Core.Workflow.OrOperation();
            Assert.True(TempOperation.Start(1).Result);
        }

        [Fact]
        public void StartOneFailedOperation()
        {
            Copernicus.Core.Workflow.OrOperation TempOperation = new Core.Workflow.OrOperation();
            TempOperation.SuccessOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => x.Value + 1 });
            TempOperation.SuccessOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => { throw new ArgumentException("A"); } });
            TempOperation.FailureOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => x.Value + 1 });
            Assert.True(TempOperation.Start(1).Result);
        }

        [Fact]
        public void StartOneSubOperation()
        {
            Copernicus.Core.Workflow.OrOperation TempOperation = new Core.Workflow.OrOperation();
            TempOperation.SuccessOperations.Add(new Copernicus.Core.Workflow.GenericOperation() { InternalOperation = x => x.Value + 1 });
            Assert.True(TempOperation.Start(1).Result);
        }
    }
}