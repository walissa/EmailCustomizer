<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
                xmlns:msxsl="urn:schemas-microsoft-com:xslt"
                xmlns:s0="http://OrderAcknowledgement.Schemas.Internal.OrderAck"
                exclude-result-prefixes="msxsl s0" version="1.0">

  <xsl:output omit-xml-declaration="yes" method="html" indent="yes"  encoding="utf-8" version="1.0" />
  <xsl:template match="/">
    <xsl:apply-templates select="s0:OrderAcknowledgement"/>
  </xsl:template>
  <xsl:template match="s0:OrderAcknowledgement" >
    <html>
      <head>
        <title>Order Response Error</title>
        <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
          <style>
            /* Font Definitions */
            @font-face {
            font-family: "Cambria Math";
            }

            @font-face {
            font-family: Calibri;
            }
            /* Style Definitions */
            p.MsoNormal, li.MsoNormal, div.MsoNormal {
            margin: 0in;
            margin-bottom: .0001pt;
            font-size: 11.0pt;
            font-family: "Calibri",sans-serif;
            }

            table.clstable {
            border-collapse:collapse;
            box-shadow: -1px 1px grey, -2px 2px grey, -3px 3px grey, -4px 4px grey, -5px 5px grey;
            }

            thead td {
            text-align:center;
            }
            table.clstable td {
            border: 1pt solid lightgray;
            padding: 0in 5.4pt 0in 5.4pt;
            }

            a:link, span.MsoHyperlink {
            color: #0563C1;
            }

            a:visited, span.MsoHyperlinkFollowed {
            color: #954F72;
            text-decoration: underline;
            }

            span.EmailStyle17 {
            font-family: "Calibri",sans-serif;
            color: windowtext;
            }


            .MsoChpDefault {
            font-family: "Calibri",sans-serif;
            }

            @page WordSection1 {
            size: 8.5in 11.0in;
            margin: 70.85pt 70.85pt 70.85pt 70.85pt;
            }

            div.WordSection1 {
            margin: 50px;
            padding: 5px;
            }
          </style>
        </meta>
      </head>
      <body>
        <div class="WordSection1">
          <p class="MsoNormal">
            The order below cannot be found in AX, the order detailed information are:
          </p>
          <p class="MsoNormal">
            <b>Buyer Account No: </b>
            <xsl:value-of select="s0:PurchaseOrderHeader/s0:BuyerAccountNo"/>
          </p>

          <p class="MsoNormal">
            <b>Order No: </b>
            <xsl:value-of select="s0:PurchaseOrderHeader/s0:OrderNo"/>
          </p>

          <p class="MsoNormal">
            <b>Sellers Order Reference: </b>
            <xsl:value-of select="s0:PurchaseOrderHeader/s0:SellersOrderReference"/>
          </p>

          <p class="MsoNormal">
            <b>Currency Code: </b>
            <xsl:value-of select="s0:PurchaseOrderHeader/s0:CurrencyCode"/>
          </p>

          <p class="MsoNormal">
            <b>
              Confirmed Order Date:
            </b>
            <xsl:value-of select="s0:PurchaseOrderHeader/s0:ConfirmedOrderDate"/>
          </p>
          <p class="MsoNormal">
            <b>Confirmation Date: </b>
            <xsl:value-of select="s0:PurchaseOrderHeader/s0:DateOfConfirmation"/>
          </p>

          <br/>
        </div>
        <div>
          <table class="clstable">
            <thead>
              <tr>
                <td style="width:50pt;">
                  <p class="MsoNormal">
                    <b>LineNo</b>
                  </p>
                </td>
                <td style="width:80pt;">
                  <p class="MsoNormal">
                    <b>ItemCode</b>
                  </p>
                </td>
                <td style="width:80pt;">
                  <p class="MsoNormal">
                    <b>ItemDescription</b>
                  </p>
                </td>
                <td style="width:80pt;">
                  <p class="MsoNormal">
                    <b>ProductName</b>
                  </p>
                </td>
                <td style="width:80pt;">
                  <p class="MsoNormal">
                    <b>Price</b>
                  </p>
                </td>
                <td style="width:80pt;">
                  <p class="MsoNormal">
                    <b>PriceUnit</b>
                  </p>
                </td>
                <td style="width:80pt;">
                  <p class="MsoNormal">
                    <b>OrderedQty</b>
                  </p>
                </td>
                <td style="width:100pt;">
                  <p class="MsoNormal">
                    <b>Confirmed Del. Date</b>
                  </p>
                </td>
              </tr>
            </thead>
            <xsl:apply-templates select="s0:PurchaseOrderLines/s0:PurchaseOrderLine"/>
          </table>

          <p class='MsoNormal'></p>

        </div>

      </body>

    </html>
  </xsl:template>
  <xsl:template match="s0:PurchaseOrderLine">
    <tbody>
      <tr>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:LineNo'/>
          </p>
        </td>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:ItemCode'/>
          </p>
        </td>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:ItemDescription'/>
          </p>
        </td>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:ProductName'/>
          </p>
        </td>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:Price'/>
          </p>
        </td>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:PriceUnit'/>
          </p>
        </td>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:OrderedQty'/>
          </p>
        </td>
        <td>
          <p class="MsoNormal">
            <xsl:value-of select='s0:ConfirmedDeliveryDate'/>
          </p>
        </td>
      </tr>
    </tbody>

  </xsl:template>

</xsl:stylesheet>