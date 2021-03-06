<Query Kind="Statements">
  <Connection>
    <ID>88a04bb7-535c-43a6-99c6-385c238126df</ID>
    <Driver Assembly="Tx.LinqPad" PublicKeyToken="3d3a4b0768c9178e">Tx.LinqPad.TxDataContextDriver</Driver>
    <DriverData>
      <ContextName>http</ContextName>
      <Files>($SampleTraces)HTTP_Server.etl;</Files>
      <MetadataFiles>($SampleTraces)HTTP_Server.man;</MetadataFiles>
      <IsRealTime>false</IsRealTime>
      <IsUsingDirectoryLookup>false</IsUsingDirectoryLookup>
    </DriverData>
  </Connection>
  <Namespace>Tx.Windows.Microsoft_Windows_HttpService</Namespace>
</Query>

var begin = playback.GetObservable<Parse>();
var end = playback.GetObservable<FastSend>();

var requests = from b in begin 
			   from e in end.Where(e=>e.Header.ActivityId == b.Header.ActivityId).Take(1)
			   select new
			   {
					b.Url,
					e.HttpStatus,
					Duration = (e.Header.Timestamp - b.Header.Timestamp).TotalMilliseconds
				};

requests.Dump();