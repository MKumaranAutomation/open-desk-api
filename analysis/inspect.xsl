<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
  <xsl:key name="ISSUETYPES" match="/Report/Issues/Project/Issue" use="@TypeId"/>
  <xsl:output method="html" indent="yes"/>

  <xsl:template match="/" name="TopLevelReport">
    <html>
	  <head>
      <title>Resharper InspectCode Report</title>
      <style>
        .severity {
            background-color: #1BA1E2;
            color: white;
            box-shadow: 4px 4px 2px grey;
            margin: 4px;
            padding: 4px;
        }

        th {
            background-color: #00ABA9;
            color: white;
            background-color: #00ABA9;
            box-shadow: 2px 2px 1px grey;
            margin: 4px;
            padding: 4px;
        }

        h1 {
            text-align: center;
            color: white;
            background-color: #00ABA9;
            box-shadow: 4px 4px 2px grey;
            margin: 4px;
            padding: 4px;
        }

        table {
            font-weight: 400;
        }
      </style>
    </head>
      <body>
        <h1>Resharper InspectCode Report</h1>

        <xsl:for-each select="/Report/IssueTypes/IssueType">
          <h2>
            <span class="severity"><xsl:value-of select="@Severity"/></span>: <xsl:value-of select="@Description"/>
          </h2>
          <table style="width:100%">
            <tr>
              <th>File</th>
              <th>Line Number</th>
              <th>Message</th>
            </tr>
            <xsl:for-each select="key('ISSUETYPES',@Id)">
              <tr>
                <td>
                  <xsl:value-of select="@File"/>
                </td>
                <td>
                  <xsl:value-of select="@Line"/>
                </td>
                <td>
                  <xsl:value-of select="@Message"/>
                </td>
              </tr>
            </xsl:for-each>
          </table>
          <hr />
          <br />
        </xsl:for-each>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>