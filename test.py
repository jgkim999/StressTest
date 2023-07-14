from org.apache.http.client.methods import *
from org.apache.http.impl.client import *
from org.apache.http.client import *
from org.apache.http import *
from org.json import *
from java.io import *
from java.util import *
from java.lang import *

client = HttpClientBuilder.create().build();
url = "https://host/api/invoices";
rq = HttpGet(url);
rq.addHeader("Authorization", "**guid**");
rq.addHeader("Accept", "application/json");
rsp = client.execute(rq);
rd = BufferedReader(InputStreamReader(rsp.getEntity().getContent()));
line = "";
sb = StringBuffer();
while True:
  line = rd.readLine()
  if line == None:
    break
  else:
    sb.append(line);

s = sb.toString();
jo = JSONObject(sb.toString());
ja = jo.get("invoices");
itr = ja.iterator();

while (itr.hasNext()):
  s2 = itr.next().toString();
  jo2 = JSONObject(s2);
  invoiceId = jo2.get("invoice_id")

  url = "https://host/api/invoices/" + str(invoiceId)
  client = HttpClientBuilder.create().build();
  rq = HttpGet(url);
  rq.addHeader("Authorization", "**guid**");
  rsp = client.execute(rq);
  ist = rsp.getEntity().getContent();
  filePath = "/opt/tomcat8/" + str(invoiceId) + ".pdf";
  fos = FileOutputStream(File(filePath));
  while True:
    inByte = ist.read()
    if inByte == -1:
      break
    else:
      fos.write(inByte);

  ist.close();
  fos.close();