/* Tigra Menu items structure */
var MENU_ITEMS = [
	['Products', null, {'sw':200,'bw':200},
		['Product Descriptions', null, {'bw':200},
			['xDoc Server / APIs', ROOT_PATH + '/products/downloads/server.htm'],
			['xDoc Converter Desktop', ROOT_PATH + '/products/downloads/desktop.htm'],
			['xDoc Structured Finance Toolkit', ROOT_PATH + '/products/downloads/StructuredFinance.htm'],
			
		],
		['Downloads', ROOT_PATH + '/products/downloads'],
		['Supported Formats', ROOT_PATH + '/products/supportedformats.htm'],
		['Technical Overview', null, {'bw':200},
			['xDoc Architecture', ROOT_PATH + '/products/overview/architecture.htm'],
			['xDoc Transformation Engine', ROOT_PATH + '/products/overview/xte.htm'],
			['Document Conversion to XML', ROOT_PATH + '/products/overview/conversion.htm'],
			['Document Publishing from XML', ROOT_PATH + '/products/overview/publishing.htm'],
			['End-to-End Conversions', ROOT_PATH + '/products/overview/endtoendconversions.htm']
		],
		['Licensing', ROOT_PATH + '/products/licensing.htm']
	],
	['Solutions', null, {'sw':200,'bw':200},
		['Enterprise Publishing', ROOT_PATH + '/solutions/enterprisepublishing.htm'],
		['Structured Documentation', null, null,
			['DocBook', ROOT_PATH + '/solutions/docbook.htm'],
			['DITA', ROOT_PATH + '/solutions/dita.htm']
		],
		['Financial Services', ROOT_PATH + '/solutions/financialservices.htm'],
		['OEMs', ROOT_PATH + '/solutions/oems.htm'],
		['System Integration', ROOT_PATH + '/products/downloads/xDocProfessional.htm']

	],
	['Services', null, {'sw':200,'bw':200},
		['XML Conversions', ROOT_PATH + '/services/conversion'],
		['Training', ROOT_PATH + '/services/training']
	],

	['Partners', ROOT_PATH + '/partners', {'sw':200}]
];
