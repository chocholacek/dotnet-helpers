namespace DotnetHelperTool.Tools
{
    public class DataSizeConverter : CLIToolSetupBase, ICLIToolSetup
    {
        public DataSizeConverter() : base("datasize", "conversions between data size units")
        {
        }

        public override void Register()
        {
            var from = new Option<DataSizeUnit>(new[] { "-f", "--from" }, () => DataSizeUnit.B);
            var to = new Option<DataSizeUnit>(new[] { "-t", "--to" }, () => DataSizeUnit.B);
            var value = new Argument<double>("value");

            Command.AddOptions(from, to);
            Command.AddArgument(value);
            Command.SetHandler<double, DataSizeUnit, DataSizeUnit>(Handle, value, from, to);
        }

        private void Handle(double value, DataSizeUnit from, DataSizeUnit to) => Console.WriteLine(DataSizeHelper.Convert(value, from, to));
    }
}