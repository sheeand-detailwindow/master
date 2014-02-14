using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using detailwindow.gov.weather.graphical;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Data;
using detailwindow.DataSet1TableAdapters;

namespace detailwindow.uc
{
    public partial class wxForcast : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    GetWeather();

                    string[,] strPeriodName = new string[7, 2];
                    string[,] strTemperature = new string[7, 2];
                    //string[,] strPrecipitation = new string[7, 2];
                    //string[,] strConditions = new string[7, 2];
                    string[] strPrecipitation = new string[14];
                    string[] strConditions = new string[14];

                    // Get weather forecast
                    //string strWxXml = "";

                    //weatherParametersType paramsWX = new detailwindow.gov.weather.graphical.weatherParametersType();
                    //ndfdXML wxXML = new detailwindow.gov.weather.graphical.ndfdXML();
                    //DateTime dtStart = DateTime.Now;
                    //Decimal longitude = 39.73M;
                    //Decimal latitude = -86.27M;
                    //formatType wxFormat = detailwindow.gov.weather.graphical.formatType.Item12hourly;
                    //unitType wxUnits = detailwindow.gov.weather.graphical.unitType.e;

                    //// Call web service proxy class
                    //using (wxXML)
                    //{
                    //    strWxXml = wxXML.NDFDgenByDay(longitude, latitude, dtStart, "7", wxUnits, wxFormat);
                    //}



                    //string strWxXmlNoColons = strWxXml.Replace(":", "");
                    //string strWxXmlNoColonsDashes = strWxXmlNoColons.Replace("-", "");
                    //string strWxXmlNoColonsDashesBackslashes = strWxXmlNoColonsDashes.Replace("\\", "");

                    //StringReader sr = new StringReader(strWxXmlNoColonsDashes);
                    //XmlTextReader tr = new XmlTextReader(sr);
                    //DataSet ds = new DataSet();
                    //ds.ReadXml(tr);

                    //int tableCount;
                    //int rowCount;
                    //int rowMax = 0;
                    //int colCount;
                    //int colMax = 0;


                    //DataTable[] dt = new DataTable[ds.Tables.Count];

                    //dt.Initialize();

                    //for (tableCount = 0; tableCount < ds.Tables.Count; tableCount++)
                    //{
                    //    dt[tableCount] = ds.Tables[tableCount];

                    //    // Find the max number of rows by getting a count of the rows in each table
                    //    if (dt[tableCount].Rows.Count > rowMax) rowMax = dt[tableCount].Rows.Count;

                    //    // Find the max number of columns by getting a count of the columns in each table
                    //    if (dt[tableCount].Columns.Count > colMax) colMax = dt[tableCount].Columns.Count;
                    //}

                    //// Construct tables
                    //for (tableCount = 0; tableCount < ds.Tables.Count; tableCount++)
                    //{
                    //    for (rowCount = 0; rowCount < dt[tableCount].Rows.Count; rowCount++)
                    //    {
                    //        for (colCount = 0; colCount < dt[tableCount].Columns.Count; colCount++)
                    //        {
                    //            dt[tableCount].Rows[rowCount][colCount] = ConvertToString(ds.Tables[tableCount].Rows[rowCount][colCount]);
                    //        }
                    //    }
                    //}

 
                    //int currentPosition = 0;
                    //int detectedStartPosition = 0;
                    //int detectedEndPosition = 0;
                    //int detectedLength = 0;
                    //int count;

                    //// Extract specific weather data

                    //// Period names
                    //count = 0;
                    //while (count < 7)
                    //{
                    //    // Detect <start-valid-time period-name="xxxxxx">
                    //    detectedStartPosition = strWxXml.IndexOf("start-valid-time period-name=", currentPosition) + 30;
                    //    detectedEndPosition = strWxXml.IndexOf(">", detectedStartPosition) - 1;
                    //    detectedLength = detectedEndPosition - detectedStartPosition;
                    //    strPeriodName[count, 0] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}

                    //count = 0;
                    //while (count < 7)
                    //{
                    //    // Detect <start-valid-time period-name="xxxxxx">
                    //    detectedStartPosition = strWxXml.IndexOf("start-valid-time period-name=", currentPosition) + 30;
                    //    detectedEndPosition = strWxXml.IndexOf(">", detectedStartPosition) - 1;
                    //    detectedLength = detectedEndPosition - detectedStartPosition;
                    //    strPeriodName[count, 1] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}

                    //// Temperatures
                    //count = 0;
                    //while (count < 7)
                    //{
                    //    detectedStartPosition = strWxXml.IndexOf("<value", currentPosition) + 7;
                    //    if (strWxXml.Substring(detectedStartPosition, 1) == "x")
                    //    {
                    //        // Detect <value xsi:nil="true"/>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strTemperature[count, 0] = "";
                    //    }
                    //    else
                    //    {
                    //        // Detect <value>nn</value>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strTemperature[count, 0] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    }
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}

                    //count = 0;
                    //while (count < 7)
                    //{
                    //    detectedStartPosition = strWxXml.IndexOf("<value", currentPosition) + 7;
                    //    if (strWxXml.Substring(detectedStartPosition, 1) == "x")
                    //    {
                    //        // Detect <value xsi:nil="true"/>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strTemperature[count, 1] = "";
                    //    }
                    //    else
                    //    {
                    //        // Detect <value>nn</value>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strTemperature[count, 1] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    }
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}

                    //// Precipitation
                    //count = 0;
                    //while (count < 7)
                    //{
                    //    detectedStartPosition = strWxXml.IndexOf("<value", currentPosition) + 7;
                    //    if (strWxXml.Substring(detectedStartPosition, 1) == "x")
                    //    {
                    //        // Detect <value xsi:nil="true"/>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strPrecipitation[count, 0] = "";
                    //    }
                    //    else
                    //    {
                    //        // Detect <value>nn</value>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strPrecipitation[count, 0] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    }
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}

                    //count = 0;
                    //while (count < 7)
                    //{
                    //    detectedStartPosition = strWxXml.IndexOf("<value", currentPosition) + 7;
                    //    if (strWxXml.Substring(detectedStartPosition, 1) == "x")
                    //    {
                    //        // Detect <value xsi:nil="true"/>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strPrecipitation[count, 1] = "";
                    //    }
                    //    else
                    //    {
                    //        // Detect <value>nn</value>
                    //        detectedEndPosition = strWxXml.IndexOf("<", detectedStartPosition);
                    //        detectedLength = detectedEndPosition - detectedStartPosition;
                    //        strPrecipitation[count, 1] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    }
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}

                    //// Conditions
                    //count = 0;
                    //while (count < 7)
                    //{
                    //    // Detect <weather-conditions weather-summary="xxxxxx"/>
                    //    detectedStartPosition = strWxXml.IndexOf("weather-summary=", currentPosition) + 17;
                    //    detectedEndPosition = strWxXml.IndexOf('"', detectedStartPosition);
                    //    detectedLength = detectedEndPosition - detectedStartPosition;
                    //    strConditions[count, 0] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}

                    //count = 0;
                    //while (count < 7)
                    //{
                    //    // Detect <weather-conditions weather-summary="xxxxxx"/>
                    //    detectedStartPosition = strWxXml.IndexOf("weather-summary=", currentPosition) + 17;
                    //    detectedEndPosition = strWxXml.IndexOf('"', detectedStartPosition);
                    //    detectedLength = detectedEndPosition - detectedStartPosition;
                    //    strConditions[count, 1] = strWxXml.Substring(detectedStartPosition, detectedLength);
                    //    count++;
                    //    currentPosition = detectedEndPosition;
                    //}


                    // Extract specific weather data from the database tables

                    DataTable dt = new DataTable();
                    int count;
                    count = 0;

                    // Period names
                    startvalidtimeTableAdapter startvalidtime = new startvalidtimeTableAdapter();
                    dt = startvalidtime.GetPeriodNamesDaytime();
                    for (count = 0; count < 7; count++)
                    {
                        strPeriodName[count, 0] = dt.Rows[count]["periodname"].ToString();
                    }


                    // Temperatures
                    valueTableAdapter value = new valueTableAdapter();
                    dt = value.GetHighTemperature();
                    for (count = 0; count < 7; count++)
                    {
                        strTemperature[count, 0] = dt.Rows[count]["value_Text"].ToString();
                    }

                    // Precipitation
                    dt = value.GetProbOfPrecip();
                    int start;
                    // Today's probability is always on data base row 16, or dt row 15
                    // Daytime probobilities are on the even rows
                    start = 14;
                    int count2 = 0;
                    for (count = start; count < 25; count = count + 2)
                    {
                        strPrecipitation[count2] = dt.Rows[count]["precip"].ToString();
                        count2++;
                    }


                    // Conditions
                    weatherconditionsTableAdapter weatherconditions = new weatherconditionsTableAdapter();
                    dt = weatherconditions.GetWeatherSummary();
                    // Daytime conditions are always the even data base rows or odd dt rows
                    start = 0;
                    count2 = 0;
                    for (count = start; count < 14; count = count + 2)
                    {
                        strConditions[count2] = dt.Rows[count]["weathersummary"].ToString();
                        count2++;
                    }


                    // Populate label controls

                    lblFirstDayPeriod.Text = strPeriodName[0, 0];
                    lblFirstDayTemp.Text = strTemperature[0, 0];
                    lblFirstDayPrecip.Text = strPrecipitation[0];
                    lblFirstDayCond.Text = strConditions[0];
                    //lblFirstDayPrecip.Text = strPrecipitation[0, 0];
                    //lblFirstDayCond.Text = strConditions[0, 0];

                    lblSecondDayPeriod.Text = strPeriodName[1, 0];
                    lblSecondDayTemp.Text = strTemperature[1, 0];
                    lblSecondDayPrecip.Text = strPrecipitation[1];
                    lblSecondDayCond.Text = strConditions[1];
                    //lblSecondDayPrecip.Text = strPrecipitation[1, 0];
                    //lblSecondDayCond.Text = strConditions[1, 0];

                    lblThirdDayPeriod.Text = strPeriodName[2, 0];
                    lblThirdDayTemp.Text = strTemperature[2, 0];
                    lblThirdDayPrecip.Text = strPrecipitation[2];
                    lblThirdDayCond.Text = strConditions[2];
                    //lblThirdDayPrecip.Text = strPrecipitation[2, 0];
                    //lblThirdDayCond.Text = strConditions[2, 0];

                    lblForthDayPeriod.Text = strPeriodName[3, 0];
                    lblForthDayTemp.Text = strTemperature[3, 0];
                    lblForthDayPrecip.Text = strPrecipitation[3];
                    lblForthDayCond.Text = strConditions[3];
                    //lblForthDayPrecip.Text = strPrecipitation[3, 0];
                    //lblForthDayCond.Text = strConditions[3, 0];

                    lblFifthDayPeriod.Text = strPeriodName[4, 0];
                    lblFifthDayTemp.Text = strTemperature[4, 0];
                    lblFifthDayPrecip.Text = strPrecipitation[4];
                    lblFifthDayCond.Text = strConditions[4];
                    //lblFifthDayPrecip.Text = strPrecipitation[4, 0];
                    //lblFifthDayCond.Text = strConditions[4, 0];

                    //lblSixthDayPeriod.Text = strPeriodName[5, 0];
                    //lblSixthDayTemp.Text = strTemperature[5, 0];
                    //lblSixthDayPrecip.Text = strPrecipitation[5];
                    //lblSixthDayCond.Text = strConditions[5];
                    //lblSeventhDayPrecip.Text = strPrecipitation[6];
                    //lblSeventhDayCond.Text = strConditions[6];
                    //lblSixthDayPrecip.Text = strPrecipitation[5, 0];
                    //lblSixthDayCond.Text = strConditions[5, 0];

                    //lblSeventhDayPeriod.Text = strPeriodName[6, 0];
                    //lblSeventhDayTemp.Text = strTemperature[6, 0];
                    //lblSeventhDayPrecip.Text = strPrecipitation[6];
                    //lblSeventhDayCond.Text = strConditions[6];
                    //lblSeventhDayPrecip.Text = strPrecipitation[6, 0];
                    //lblSeventhDayCond.Text = strConditions[6, 0];
                }
                catch
                {
                    // WX is out of service
                    WxExtendedPanel.Visible = false;
                }
            }
        }

        protected void GetWeather()
        {
            // Get weather forecast


            string strWxXml = "";

            weatherParametersType paramsWX = new detailwindow.gov.weather.graphical.weatherParametersType();
            ndfdXML wxXML = new detailwindow.gov.weather.graphical.ndfdXML();
            DateTime dtStart = DateTime.Now;
            Decimal longitude = 39.73M;
            Decimal latitude = -86.27M;
            formatType wxFormat = detailwindow.gov.weather.graphical.formatType.Item12hourly;
            unitType wxUnits = detailwindow.gov.weather.graphical.unitType.e;

            // Call web service proxy class
            using (wxXML)
            {
                strWxXml = wxXML.NDFDgenByDay(longitude, latitude, dtStart, "7", wxUnits, wxFormat);
            }

            // Clean-up the response string
            string strWxXmlNoColons = strWxXml.Replace(":", "");
            string strWxXmlNoColonsDashes = strWxXmlNoColons.Replace("-", "");
            string strWxXmlNoColonsDashesBackslashes = strWxXmlNoColonsDashes.Replace("\\", "");

            // Deserialize the string into a DataSet
            StringReader sr = new StringReader(strWxXmlNoColonsDashes);
            XmlTextReader tr = new XmlTextReader(sr);
            DataSet ds = new DataSet();
            DataSet1 ds_xsd = new DataSet1();

            // XmlTextReader tr2 = new XmlTextReader("../util/DWML.xsd");

            // The dataset containing the NDFDgenByDay data, deserialized and cleaned-up
            ds.ReadXml(tr);

            // -------------------------------------

            // The database tables were built by examining the structure of the tables in dataset ds 
            // So, for each pertanent database table, rows are inserted with explicitly named values
            // This guarentees the data will be found in the correct columns because the column order in ds is not consistent
            // String arrays are used for convenience and debugging

            string[] columnName = new string[12];
            string[] columnValue = new string[12];
            int columnCount;
            int j;


            timelayoutTableAdapter timelayout = new timelayoutTableAdapter();
            timelayout.DeleteQuery();
            columnCount = ds.Tables["timelayout"].Columns.Count;
            for (j = 0; j < ds.Tables["timelayout"].Rows.Count; j++)
            {
                columnValue[0] = ds.Tables["timelayout"].Rows[j]["layoutkey"].ToString();
                columnValue[1] = ds.Tables["timelayout"].Rows[j]["timelayout_Id"].ToString();
                columnValue[2] = ds.Tables["timelayout"].Rows[j]["timecoordinate"].ToString();
                columnValue[3] = ds.Tables["timelayout"].Rows[j]["summarization"].ToString();
                columnValue[4] = ds.Tables["timelayout"].Rows[j]["data_Id"].ToString();
                timelayout.Insert(columnValue[0], columnValue[1], columnValue[2], columnValue[3], columnValue[4]);
            }

            startvalidtimeTableAdapter startvalidtime = new startvalidtimeTableAdapter();
            startvalidtime.DeleteQuery();
            columnCount = ds.Tables["startvalidtime"].Columns.Count;
            for (j = 0; j < ds.Tables["startvalidtime"].Rows.Count; j++)
            {
                columnValue[0] = ds.Tables["startvalidtime"].Rows[j]["periodname"].ToString();
                columnValue[1] = ds.Tables["startvalidtime"].Rows[j]["startvalidtime_Text"].ToString();
                columnValue[2] = ds.Tables["startvalidtime"].Rows[j]["timelayout_Id"].ToString();
                startvalidtime.Insert(columnValue[0], columnValue[1], columnValue[2]);
            }


            temperatureTableAdapter temperature = new temperatureTableAdapter();
            temperature.DeleteQuery();
            columnCount = ds.Tables["temperature"].Columns.Count;
            for (j = 0; j < ds.Tables["temperature"].Rows.Count; j++)
            {
                columnValue[0] = ds.Tables["temperature"].Rows[j]["name"].ToString();
                columnValue[1] = ds.Tables["temperature"].Rows[j]["temperature_Id"].ToString();
                columnValue[2] = ds.Tables["temperature"].Rows[j]["type"].ToString();
                columnValue[3] = ds.Tables["temperature"].Rows[j]["units"].ToString();
                columnValue[4] = ds.Tables["temperature"].Rows[j]["timelayout"].ToString();
                columnValue[5] = ds.Tables["temperature"].Rows[j]["parameters_Id"].ToString();
                temperature.Insert(columnValue[0], columnValue[1], columnValue[2], columnValue[3], columnValue[4], columnValue[5]);
            }

            valueTableAdapter value = new valueTableAdapter();
            value.DeleteQuery();
            columnCount = ds.Tables["value"].Columns.Count;
            for (j = 0; j < ds.Tables["value"].Rows.Count; j++)
            {
                //columnValue[0] = ds.Tables["value"].Rows[j]["xsinil"].ToString();
                columnValue[0] = ds.Tables["value"].Rows[j]["value_Text"].ToString();
                columnValue[1] = ds.Tables["value"].Rows[j]["temperature_Id"].ToString();
                columnValue[2] = ds.Tables["value"].Rows[j]["probabilityofprecipitation_Id"].ToString();
                columnValue[3] = ds.Tables["value"].Rows[j]["coverage"].ToString();
                columnValue[4] = ds.Tables["value"].Rows[j]["intensity"].ToString();
                columnValue[5] = ds.Tables["value"].Rows[j]["weathertype"].ToString();
                columnValue[6] = ds.Tables["value"].Rows[j]["qualifier"].ToString();
                columnValue[7] = ds.Tables["value"].Rows[j]["additive"].ToString();
                columnValue[8] = ds.Tables["value"].Rows[j]["weatherconditions_Id"].ToString();
                value.Insert(columnValue[0], columnValue[1], columnValue[2], columnValue[3], columnValue[4], columnValue[5], columnValue[6], columnValue[7], columnValue[8]);
            }

            probabilityofprecipitationTableAdapter probabilityofprecipitation = new probabilityofprecipitationTableAdapter();
            probabilityofprecipitation.DeleteQuery();
            columnCount = ds.Tables["probabilityofprecipitation"].Columns.Count;
            for (j = 0; j < ds.Tables["probabilityofprecipitation"].Rows.Count; j++)
            {
                columnValue[0] = ds.Tables["probabilityofprecipitation"].Rows[j]["name"].ToString();
                columnValue[1] = ds.Tables["probabilityofprecipitation"].Rows[j]["probabilityofprecipitation_Id"].ToString();
                columnValue[2] = ds.Tables["probabilityofprecipitation"].Rows[j]["type"].ToString();
                columnValue[3] = ds.Tables["probabilityofprecipitation"].Rows[j]["units"].ToString();
                columnValue[4] = ds.Tables["probabilityofprecipitation"].Rows[j]["timelayout"].ToString();
                columnValue[5] = ds.Tables["probabilityofprecipitation"].Rows[j]["parameters_Id"].ToString();
                probabilityofprecipitation.Insert(columnValue[0], columnValue[1], columnValue[2], columnValue[3], columnValue[4], columnValue[5]);
            }


            weatherconditionsTableAdapter weatherconditions = new weatherconditionsTableAdapter();
            weatherconditions.DeleteQuery();
            columnCount = ds.Tables["weatherconditions"].Columns.Count;
            for (j = 0; j < ds.Tables["weatherconditions"].Rows.Count; j++)
            {
                columnValue[0] = ds.Tables["weatherconditions"].Rows[j]["weatherconditions_Id"].ToString();
                columnValue[1] = ds.Tables["weatherconditions"].Rows[j]["weathersummary"].ToString();
                columnValue[2] = ds.Tables["weatherconditions"].Rows[j]["xsinil"].ToString();
                columnValue[3] = ds.Tables["weatherconditions"].Rows[j]["weather_Id"].ToString();
                weatherconditions.Insert(columnValue[0], columnValue[1], columnValue[2], columnValue[3]);
            }
        }
    }
}