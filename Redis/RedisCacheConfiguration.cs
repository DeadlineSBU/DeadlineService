namespace Deadline.redis
{
    public class RedisCacheConfiguration
    {
        public string Connection { get; set; }
        public string InstanceName { get; set; }
        public int DbNumber { get; set; }
        public bool UseFromInstanceNameInKey { get; set; }
    }
}