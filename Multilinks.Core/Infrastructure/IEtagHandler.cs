namespace Multilinks.Core.Infrastructure
{
   public interface IEtagHandlerFeature
   {
      bool NoneMatch(IEtaggable entity);
   }
}
