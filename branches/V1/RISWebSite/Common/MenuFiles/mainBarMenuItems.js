/* Tigra Menu items structure */
var MENU_ITEMS = [
	['Products', null, {'sw':100,'bw':125},
		['Product Downloads', null, {'bw':125},
			['xDoc Pro', ROOT_PATH + '/products/downloads/xDocPro.htm'],
			['xDoc View', ROOT_PATH + '/products/downloads/xDocView.htm'],
/*			['xDoc Structured Finance Toolkit', ROOT_PATH + '/products/downloads/StructuredFinance.htm'],*/
			
		],
/*		['Downloads', ROOT_PATH + '/products/downloads'],*/
/*		['Supported Formats', ROOT_PATH + '/products/supportedformats.htm'], MB-- Need to clean up all content in products/overview*/
/*		['Technical Overview', null, {'bw':125},
			['xDoc Architecture', ROOT_PATH + '/products/overview/architecture.htm'],
			['xDoc Transformation Engine', ROOT_PATH + '/products/overview/xte.htm'],
			['Document Conversion to XML', ROOT_PATH + '/products/overview/conversion.htm'],
			['Document Publishing from XML', ROOT_PATH + '/products/overview/publishing.htm'],
			['End-to-End Conversions', ROOT_PATH + '/products/overview/endtoendconversions.htm']
		], */
		['Licensing', ROOT_PATH + '/products/licensing.htm']
	],
	['Solutions', null, {'sw':100,'bw':125},
		['Enterprise Publishing', ROOT_PATH + '/solutions/enterprisepublishing.htm'],
		['Structured Documentation', null, null,
			['DocBook', ROOT_PATH + '/solutions/docbook.htm'],
			['DITA', ROOT_PATH + '/solutions/dita.htm']
		],
		['Financial Services', ROOT_PATH + '/solutions/financialservices.htm'],
		['OEMs', ROOT_PATH + '/partners/OEM.htm'],
		['System Integration', ROOT_PATH + '/partners/SystemIntegrators.htm']

	],
	['Services', null, {'sw':100,'bw':125},
		['XML Conversions', ROOT_PATH + '/services/conversion'],
		['Training', ROOT_PATH + '/services/training']
	],

	['Partners', ROOT_PATH + '/partners', {'sw':100}],
	
	['Resources', null, {'sw':100,'bw':125},
		['White Papers', null, {'bw':200},
			['Transforming Unstructured Content to XML', ROOT_PATH + '/resources/whitepapers/id16.htm'],
			['Why Convert Documents to XML', ROOT_PATH + '/resources/whitepapers/id35.htm'],
			['7 Deadly Sins of XML Publishing', ROOT_PATH + '/resources/whitepapers/sevendeadlysins.htm']
		],
/*		['Case Studies', ROOT_PATH + '/resources/casestudies']*/
	],
	['News', ROOT_PATH + '/news', {'sw':100,'bw':125}],
/*	['Support', ROOT_PATH + '/support', {'sw':100,'bw':125}],*/
	['Company', null, {'sw':100,'bw':125},
		['About', ROOT_PATH + '/company/about.htm'],
		['Management Team', ROOT_PATH + '/company/mgmt.htm'],
		['Contact Us', ROOT_PATH + '/company/contact.htm']
	]
];
