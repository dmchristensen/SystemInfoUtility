<?xml version="1.0" encoding="utf-8"?> 
<xsl:stylesheet version="1.0" 
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:wix="http://schemas.microsoft.com/wix/2006/wi"
  xmlns:util="http://schemas.microsoft.com/wix/UtilExtension">

  <xsl:output method="xml" indent="yes" />

  <xsl:template match="@*|node()">
    <xsl:copy>
      <xsl:apply-templates select="@*|node()"/>
    </xsl:copy>
  </xsl:template>

  <!-- Ignore this because it is declared in the Product.wxs, we pull the version from that to name the files -->
  <xsl:key name="exe-search" match="wix:Component[contains(wix:File/@Source, 'SystemInfoUtility.exe')]" use="@Id" />

  <!-- Don't include the pbd files in the deployment packages -->
  <xsl:key
        name="pdb-to-remove"
        match="wix:Component[ substring( wix:File/@Source, string-length( wix:File/@Source ) - 3 ) = '.pdb' ]"
        use="@Id"
    />
  
  <xsl:template match="wix:Component[key('exe-search', @Id)]" />
  <xsl:template match="wix:ComponentRef[key('exe-search', @Id)]" />

  <xsl:template match="*[ self::wix:Component or self::wix:ComponentRef ][ key( 'pdb-to-remove', @Id ) ]" />

</xsl:stylesheet>