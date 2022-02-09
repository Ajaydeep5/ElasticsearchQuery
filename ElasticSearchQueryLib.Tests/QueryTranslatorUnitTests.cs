using NUnit.Framework;
using ElasticsearchQuery;
using System.Linq.Expressions;
using System.Linq;
using System;
using System.Collections.Generic;
using Nest;

namespace ElasticSearchQuery.Tests
{
    public class QueryTranslatorUnitTests
    {
        private QueryTranslator queryTranslator;
        private List<MockModel> model = new List<MockModel>();

        [SetUp]
        public void Setup()
        {
            queryTranslator = new QueryTranslator();
        }

        [Test]
        public void VisitConstant_ConstantExpressionGiven_ChangesQueryTranslatorObjectValuePropToConstant()
        {
            //Arrange            
            object constant = 42.6;
            var constantExpression = Expression.Constant(constant);

            //Act
            queryTranslator.Visit(constantExpression);

            //Assert
            Assert.IsTrue(queryTranslator.Value == constant);
        }

        [Test]
        public void VisitMethodCall_MethodCallExpressionWithWhereClauseGiven_UpdatesQueryTranslatorObjectSerachQueryProptWithNestQuery()
        {
            MockData.AddData(model);
            IQueryable < MockModel > query = model.AsQueryable();
            query = query.Where(x => x.Id == 31);
            
            queryTranslator.Visit((MethodCallExpression)query.Expression);

            var termQuery = ((IQueryContainer)queryTranslator.SearchRequest.Query).Term;
            Assert.Equals(termQuery.Value, 31);
            Assert.Equals(termQuery.Field.ToString(), "Id");


        }

        /*[Test]
        public void VisitMember_MemberExpressionGiven_ChangesQueryTranslatorObjectFieldPropToMember()
        {
            //Arrange            
            object member = "hkjhk";
            MemberExpression memberExpression = "x";

            //Act
            queryTranslator.Visit(constantExpression);

            //Assert
            Assert.IsTrue(queryTranslator.value == constant);
        }

        [Test]
        public void SetQuery__ReturnsNestQueryContainerWithRespectiveTermQuery()
        {
            queryTranslator.Visit(Expression.con)
            queryTranslator.binaryExpType = ExpressionType.Equal;

            var actual = queryTranslator.SetQuery();
            actual
        }*/

        [Test]
        public void Test()
        {
            // Arrange

            // Act
            
            // Arrange
        }
    }
}