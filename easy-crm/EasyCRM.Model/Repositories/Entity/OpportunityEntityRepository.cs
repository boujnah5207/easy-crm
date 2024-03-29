﻿using System.Collections.Generic;
using System.Linq;
using EasyCRM.Model.Domains;
using System.Linq.Expressions;
using System;

namespace EasyCRM.Model.Repositories.Entity
{
    public class OpportunityEntityRepository : IOpportunityRepository
    {
        private EasyCRMDBEntities _entities = EntityRepository.GetEntities();

        #region IOpportunityRepository Members
        public Opportunity Create(Opportunity opportunityToCreate)
        {
            _entities.AddToOpportunitySet(opportunityToCreate);
            _entities.SaveChanges();
            return opportunityToCreate;

        }

        public void Delete(Opportunity opportunityToDelete)
        {
            var originalOpportunity = Get(opportunityToDelete.Id);
            _entities.DeleteObject(originalOpportunity);
            _entities.SaveChanges();

        }

        public Opportunity Update(Opportunity opportunityToUpdate)
        {
            var originalOpportunity = Get(opportunityToUpdate.Id);
            _entities.ApplyCurrentValues(originalOpportunity.EntityKey.EntitySetName, opportunityToUpdate);
            _entities.SaveChanges();
            return opportunityToUpdate;

        }

        public Opportunity Get(int id)
        {
            return _entities.OpportunitySet.First(opportunity => opportunity.Id == id);
        }

        public IEnumerable<Opportunity> ListAll()
        {
            return _entities.OpportunitySet.ToList();
        }

        public IEnumerable<Opportunity> ListAllByCriteria(Expression<Func<Opportunity, bool>> predicate)
        {
            return _entities.OpportunitySet.Where(predicate);
        }
        #endregion
    }
}
