# The script MUST contain a function named azureml_main
# which is the entry point for this module.

import pandas as pd
import urllib.request
import json

# The entry point function can contain up to two input arguments:
#   Param<dataframe1>: a pandas.DataFrame
#   Param<dataframe2>: a pandas.DataFrame
def azureml_main(dataframe1 = None, dataframe2 = None):
    influx_db_url = dataframe1.iloc[0, 0]
    web_url = urllib.request.urlopen(influx_db_url)
    response = web_url.read()
    encoding = web_url.info().get_content_charset('utf-8')
    influx_data = json.loads(response.decode(encoding))['results']

    try:
      series = influx_data[0]['series'][0]
      return pd.DataFrame(series['values'], columns = series['columns'])
    except:
      return pd.DataFrame([], columns = ['time', 'value'])

