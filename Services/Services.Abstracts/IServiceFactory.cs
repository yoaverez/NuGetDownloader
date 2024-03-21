namespace Services.Abstracts
{
    /// <summary>
    /// A contract for factories that create services.
    /// </summary>
    public interface IServiceFactory
    {
        /// <returns>A an instance of the <see cref="ISearchService"/> class.</returns>
        ISearchService GetSearchService();

        /// <returns>A an instance of the <see cref="IPackageContentsService"/> class.</returns>
        IPackageContentsService GetPackageContentsService();

        /// <returns>A an instance of the <see cref="IPackageMetaDataService"/> class.</returns>
        IPackageMetaDataService GetPackageMetaDataService();
    }
}
