namespace Module.CommonModule.Interfaces
{
    public interface ICommonService
    {
        string GetStatusCodeMapping(string source, string code, string localId);
        string MappingResource(string FuncType, string Param, string Code, string LocaleId, string DefaultDesc);
    }
}