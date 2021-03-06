using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GenericDataAccess.Interfaces
{
    /// <summary>
    /// Interface for basic CRUD methods for Entity Framework 6 / EFCore.
    /// </summary>
    /// <typeparam name="TEntity">Class Type that has a coresponding DBSet in database.</typeparam>
    public interface IDataAccess<TEntity> : IDisposable where TEntity : class
    {
        /// <summary>
        /// Validates Entities and Commits changes to database
        /// </summary>
        /// <returns>count of entities affected in commit</returns>
        int Save();

        /// <summary>
        /// Event handler for the SavingChanges Event in DbContext.Checks to see if it is derived from GenericDataAccess.Context.Database.DbSetBase and then updates the ModifiedOn property.
        /// </summary>
        void OnSavingHandler(object sender, SavingChangesEventArgs e);

       //Read

        /// <summary>
        /// Returns a single entity from database using the Primary Key(s) of the entry.
        /// </summary>
        /// <param name="pkey"><object[] of primary key values</param>
        /// <returns>instance of entry with the supllied key(s), null if none found</returns>
        TEntity Find(params object[] pkey);

        /// <summary>
        /// Returns a single entity from database using a supplied lambda filter .
        /// </summary>
        /// <param name="predicate">Lambda Expression used to filter results</param>
        /// <returns>instance of entry that matches filter, null if none found</returns>
        TEntity FindWhere(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to get all entries in a database of the provided <typeparamref name="TEntity"/>
        /// </summary>
        /// <returns>List of Entries in the database, empty List if none found</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// Used to get all entries in a database of the provided <typeparamref name="TEntity"/> filtered by the supplied lamba expression
        /// </summary>
        /// <param name="predicate">Lambda Expression used to filter results</param>
        /// <returns>List of Entries in the database filtered by Lambda Expression, Empty List if none found</returns>
        List<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Used to get an ordered List of entries in a database of the provided <typeparamref name="TEntity"/> filtered by the supplied lamba expression and ordered by a separate lambda expression
        /// </summary>
        /// <param name="predicate">Lambda Expression used to filter results</param>
        /// <param name="orderBy">Lambda Expression used to order results</param>
        /// <param name="ascending">True, orders in ascedning orderl False, orders in descending order</param>
        /// <returns>Ordered List of Entries in the database filtered by Lambda Expression, Empty List if none found</returns>
        List<TEntity> GetOrderedWhere(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> orderBy, bool ascending = true);

        //Update


        void AddOrUpdate(ref TEntity objToUpdate);


        void AddOrUpdate(ref List<TEntity> objsToUpdate);



        //Destroy


        void Delete(ref TEntity objToDelete);
    }
}
