update tTemplates 
  SET Text = REPLACE(TEXT,'','''');

update tTemplates 
  SET Text = REPLACE(Text,'<description>','<description><![CDATA[');
  
update tTemplates 
  SET Text = REPLACE(Text,'</description>',']]></description>');

update tTemplates 
  SET Text = REPLACE(Text,'<heading>','<heading><![CDATA[');
  
update tTemplates 
  SET Text = REPLACE(Text,'</heading>',']]></heading>');

update tTemplates 
  SET Text = REPLACE(Text,'<impression>','<impression><![CDATA[');
  
update tTemplates 
  SET Text = REPLACE(Text,'</impression>',']]></impression>');

DECLARE @TemplatesXML TABLE
(
  TemplateId1 int,
  Data xml
);

INSert into @TemplatesXML
SELECT TemplateId,[Text] from tTemplates;
--where templateid <=109;

update t1 
set Heading = CAST(Data.query('/data/heading/text()') AS VARCHAR(MAX)),
[Description] = CAST(Data.query('/data/description/text()') AS VARCHAR(MAX)),
[Impression] = CAST(Data.query('/data/impression/text()') AS VARCHAR(MAX))
FROM tTemplates AS t1
inner join @TemplatesXML on TemplateId1 = TemplateId;
--select Data.query('/data/heading/text()') from @TemplatesXML;

--41,44