//using Lucene.Net.Analysis.Standard;
//using Lucene.Net.Documents;
//using Lucene.Net.Index;
//using Lucene.Net.LukeNet;
//using Lucene.Net.QueryParsers;
//using Lucene.Net.Search;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Text.RegularExpressions;
//using ZU.Core;
//using ZU.Semantic.Core;

//namespace ZU.Semantic.Keyphrases.Extractor
//{
//	public static class HighFreqTermsExtractor
//	{
		
//		private static int defaultNumTerms = 100;
//		private static int maxResults = 100;

//		public static int DefaultNumTerms
//		{
//			get
//			{ return defaultNumTerms; }
//			set
//			{ defaultNumTerms = value; }
//		}

//		public static List<string> englishStopWords
//		{
//			get;
//			set;
//		}

//		public static int MaxResults
//		{
//			get
//			{ return maxResults; }
//			set
//			{ maxResults = value; }
//		}

//		public static bool firstLoad = true;

//		public static Regex Pattern;

//		//
//		public static bool TryGetHighFreqTerms(IEntity clusterEntity, string field, ISystemInformationModel SIM, int numberOfTerms, out List<Keyphrase> termInfo)
//		{
//			// initializing
//			termInfo = new List<Keyphrase>();

//			if (firstLoad)
//			{
//				englishStopWords = new List<string>();

//				System.Reflection.Assembly asm = Assembly.GetAssembly(typeof(HighFreqTermsExtractor));

//				// Stop words list for English language from here: http://members.unine.ch/jacques.savoy/clef/index.html
//				using (var sReader = new StreamReader(asm.GetManifestResourceStream(@"ZU.EnglishST.txt")))
//				{
//					string line;
//					while ((line = sReader.ReadLine()) != null)
//					{
//						englishStopWords.Add(line);
//					}
//				}

//				if (englishStopWords.Count > 0)
//					firstLoad = false; // prevent reloading of stop words into memory

//				// instantiating pattern
//				// see: http://stackoverflow.com/questions/2159026/regex-how-to-get-words-from-a-string-c
//				Pattern = new Regex(
//					@"( [^\W_\d]              # starting with a letter
//												# followed by a run of either...
//						  ( [^\W_\d] |          #   more letters or
//							[-'\d](?=[^\W_\d])  #   ', -, or digit followed by a letter
//						  )*
//						  [^\W_\d]              # and finishing with a letter
//						)",
//					RegexOptions.IgnorePatternWhitespace);
//			}

//			// checking for nulls
//			if (SIM == null)
//				return false;

//			// saving reference to the Full-Text Index Cache folder location
//			var FullTextIndexCache = SIM.FullTextIndexCache;

//			// checking for nulls etc.
//			if (clusterEntity==null || clusterEntity.ModelContext==null)
//				return false;

//			if (string.IsNullOrEmpty(clusterEntity.ModelContext.Id))
//				return false;

//			if (!System.IO.Directory.Exists(FullTextIndexCache))
//				return false;

//			if (numberOfTerms<0)
//				numberOfTerms = defaultNumTerms;

//			#region Preparing Lucene.Net Infrastructure
//			// Init service
//			var _indexDirectory = Lucene.Net.Store.FSDirectory.Open(FullTextIndexCache);

//			// check if index exists or not
//			if (!IndexReader.IndexExists(_indexDirectory))
//				return false;
			
//			// reader (for TermFreqVector)
//			var reader = Lucene.Net.Index.IndexReader.Open(_indexDirectory, true);

//			var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
//			#endregion

//			// We need to construct initial query; for that we need to obtain Model of the given cluster (because some clusters, like known ones, have same id across all spaces)
//			var modelId = clusterEntity.ModelContext.Id;

//			// Visual Cluster Id
//			var visualClusterId = clusterEntity.Id;

//			// now we need to do a query
//			var propsToQuery = new List<string>();
//			// we are interested in Document Id
//			//propsToQuery.Add(Constants.Indexers.FullTextSearchIndexer.DocumentId);
//			// and visual cluster Id
//			propsToQuery.Add(Constants.Indexers.FullTextSearchIndexer.VisualClusterId);

//			var pToq = propsToQuery.ToArray();

//			MultiFieldQueryParser parser = new MultiFieldQueryParser(
//				Lucene.Net.Util.Version.LUCENE_30, pToq, analyzer);

//			// such query should give all cases where documents are within the given model AND belonging to the given Visual Cluster
//			string searchQuery = //Constants.Indexers.FullTextSearchIndexer.DocumentId + ":" + modelId + " AND "
//				Constants.Indexers.FullTextSearchIndexer.VisualClusterId + ":" + visualClusterId;

//			var query = parser.Parse(searchQuery);

//			var searcher = new IndexSearcher(_indexDirectory);

//			TopDocs topDocs = searcher.Search(query, maxResults);

//			int res = topDocs.ScoreDocs.Length;

//			// we should be sure that we have maxResults less than number of search results
//			if (res < maxResults)
//				maxResults = res;

//			List<ITermFreqVector> tfvs = new List<ITermFreqVector>();

//			// iterating over search results
//			for (int i = 0; i < maxResults; i++)
//			{
//				ScoreDoc scoreDoc = topDocs.ScoreDocs[i];
//				float score = scoreDoc.Score;
//				int docId = scoreDoc.Doc;
//				Document doc = searcher.Doc(docId);

//				// we need to filter content from other places
//				var entityIdPlusModelIdField = doc.GetField(Constants.Indexers.FullTextSearchIndexer.DocumentId).StringValue;

//				var foundDocModelId = entityIdPlusModelIdField.Substring(37, 36);

//				// filtering
//				if (foundDocModelId.ToLower() == modelId.ToLower())
//				{
//					// let's obtain TermFreqVector for a given field (FullText of Keyphrases)
//					if (field == Constants.Indexers.FullTextSearchIndexer.Keyphrases
//						||
//						field == Constants.Indexers.FullTextSearchIndexer.FullText)
//					{
//						var tfv = reader.GetTermFreqVector(docId, field);

//						// adding to the list
//						tfvs.Add(tfv);
//					}
//				}
//			}

//			// we have to do DocFreq() by ourselves, so...

//			// okay, we have two options now. One is to continue trying adjusting Lucene.Net's index, term vectors, and so on
//			// second option is to use that Japan-based keyphrase extraction algorithm. Could we use the full-text search data for that?

//			// DocFreq(Term term): Returns the number of documents containing the term.
//			// this means that we need to have the following structure: 
//			// Tuple<Term, DocFreq>, which could be a dictionary  
//			Dictionary<string, int> docFreq = new Dictionary<string, int>();

//			foreach (var tfv in tfvs)
//			{
//				var terms = tfv.GetTerms();
//				var frequencies = tfv.GetTermFrequencies();

//				for (int i = 0; i < terms.Length; i++)
//				{
//					var term = terms[i];

//					// adding to list
//					if (docFreq.ContainsKey(term))
//					{
//						// we increase our counter
//						docFreq[term]++;
//					}
//					else
//					{
//						// we need to have terms with minimal length, of, say, 3
//						if (term.Length < 3)
//							continue;

//						// next, using regex
//						if (Pattern != null)
//						{
//							// we remove cases when resulting field is not a match at all
//							if (!Pattern.IsMatch(term))
//								continue;
//						}

//						// we need to filter out stop words
//						if (englishStopWords.Contains(term.ToLower()))
//							continue;

						

//						if (frequencies.Length>i)
//							docFreq.Add(term, frequencies[i]);
//					}
//				}
//			}

//			// next step: using modified algorithm from HighFreqTerms
//			TermInfoQueue<TermInfo> tiq = new TermInfoQueue<TermInfo>(numberOfTerms);

//			int minFreq = 0;
			
//			// now we do the same algorithm as in HIghFreqTerms, but adapted to our Visual Cluster selection of items
//			foreach (var term in docFreq)
//			{
//				// Re-constructing Lucene's Term from the given text
//				Term t = new Term(Constants.Indexers.FullTextSearchIndexer.FullText, term.Key);
				
//				// terms.DocFreq()
//				if (term.Value > minFreq)
//				{
//					TermInfo top = (TermInfo)tiq.Add(new TermInfo(t, term.Value));
//					if (tiq.Size() >= numberOfTerms) 		     // if tiq overfull
//					{
//						tiq.Pop();				     // remove lowest in tiq
//						minFreq = top.DocFreq; // reset minFreq
//					}
//				}
//			}

//			// finally
//			TermInfo[] results = new TermInfo[tiq.Size()];

//			for (int i = 0; i < results.Length; i++)
//			{
//				results[results.Length - i - 1] = (TermInfo)tiq.Pop();
//			}

//			// cleaning up
//			reader.Dispose();
//			searcher.Dispose(); 
//			_indexDirectory.Dispose();

//			foreach (var ti in results)
//			{
//				termInfo.Add(ti.ToKeyphrase());
//			}
//			// saving results to the given query
//			//termInfo = results.ToList();

//			return true;
//		}

//		/// <summary>
//		/// Obtains a list of the high-freq terms for the given Visual Cluster
//		/// </summary>
//		/// <param name="clusterEntity"></param>
//		/// <param name="SIM"></param>
//		/// <param name="numberOfTerms"></param>
//		/// <param name="termInfo"></param>
//		/// <returns></returns>
//		public static bool TryGetHighFreqKeyphrases(VisualCluster cluster, ISystemInformationModel SIM, int numberOfTerms, out List<Keyphrase> termInfo)
//		{
//			// initializing
//			termInfo = new List<Keyphrase>();

//			// checking for nulls
//			if (SIM == null)
//				return false;

//			// saving reference to the Full-Text Index Cache folder location
//			var FullTextIndexCache = SIM.FullTextIndexCache;

//			// checking for nulls etc.
//			if (cluster == null || cluster.Topic == null || cluster.Topic.ModelContext == null)
//				return false;

//			if (string.IsNullOrEmpty(cluster.Topic.ModelContext.Id))
//				return false;

//			if (!System.IO.Directory.Exists(FullTextIndexCache))
//				return false;

//			if (numberOfTerms < 0)
//				numberOfTerms = defaultNumTerms;

//			// so, what do we have? Keyphrases within the cluster
//			var clusterKeyphrases = cluster.Topic.Keyphrases;

//			// We need to normalize all of this information to get a high-level understanding of situation with keyphrases
//			// There are few kinds of keyphrases in the cluster: those user added manually to the cluster itself,
//			// and those obtained automatically from other clusters. User can mark a keyphrase as deleted for the cluster,
//			// and this is the same for both automatically and manually added ones. 
//			// Yet we have a problem. We need to quickly distinguish keyphrases manually added at the cluster level from 
//			// keyphrases added at the entity level, to treat them differently. 
//			// Those added manually, or deleted manually at the cluster level, are automatically marked as being the Cluster-level
//			// keyphrases. 

//			#region These keyphrases matter: user did something to them (e.g., Added new ones, or Deleted)
//			/// <summary>
//			/// Keyphrases, manually added to Cluster
//			/// </summary>
//			var addedToCluster = 
//				clusterKeyphrases
//				.Where(k=>k.State == EntityFragmentState.Entered)
//				.Where(k=>k.IsClusterLevelKeyphrase).ToList();

//			/// <summary>
//			/// Keyphrases, marked as deleted by user
//			/// </summary>
//			var deletedFromCluster =
//				clusterKeyphrases
//				.Where(k=>k.IsLive == false) 
//				.Where(k=>k.IsClusterLevelKeyphrase).ToList();
//			#endregion

//			#region These keyphrases matter: user either left them after automated extraction, or added them to the entities 
//			/// <summary>
//			/// Keyphrases, manually added to entities
//			/// </summary>
//			var manAddedFromEntities = new List<Keyphrase>();
//			/// <summary>
//			/// Keyphrases, automatically extracted from entities
//			/// </summary>
//			var extAddedFromEntities = new List<Keyphrase>();
//			#endregion

//			var entities = cluster.Entities.ToList();

//			// We need to find ALL keyphrases that have been manually added by user
//			foreach (var entity in entities)
//			{
//				foreach (var keyphrase in entity.Keyphrases.Where(k => k.State == EntityFragmentState.Entered).ToList())
//				{
//					var kInMAFE = manAddedFromEntities.Where(k => k.Phrase.ToLower() == keyphrase.Phrase.ToLower()).FirstOrDefault();

//					if (kInMAFE != null)
//					{
//						// ok, this is a bit naive, but let's start with something
//						if (kInMAFE.Rank < keyphrase.Rank)
//							kInMAFE.Rank = keyphrase.Rank;
//						kInMAFE.DocFreq++;
//					}
//					else
//					{
//						keyphrase.DocFreq = 1;
//						manAddedFromEntities.Add(keyphrase);
//					}
//				}
//			}

//			// at this point, we know ALL keyphrases that were explicitly added by user to given entities
//			// now, we need to find out if any of the automatically extracted match those added manually -> this will increase
//			// their docFreq
//			foreach (var entity in entities)
//			{
//				foreach (var keyphrase in entity.Keyphrases.Where(k=>k.State == EntityFragmentState.Extracted).ToList())
//				{
//					var kInMAFE = manAddedFromEntities.Where(k=>k.Phrase.ToLower() == keyphrase.Phrase.ToLower()).FirstOrDefault();

//					if (kInMAFE != null)
//					{// ok, this is a bit naive, but let's start with something
//						if (kInMAFE.Rank < keyphrase.Rank)
//							kInMAFE.Rank = keyphrase.Rank;
//						kInMAFE.DocFreq++;
//					}
//					else
//					{
//						var kInEAFE = extAddedFromEntities.Where(k => k.Phrase.ToLower() == keyphrase.Phrase.ToLower()).FirstOrDefault();

//						if (kInEAFE != null)
//						{
//							// ok, this is a bit naive, but let's start with something
//							if (kInEAFE.Rank < keyphrase.Rank)
//								kInEAFE.Rank = keyphrase.Rank;
//							kInEAFE.DocFreq++;
//						}
//						else
//						{
//							keyphrase.DocFreq = 1;
//							extAddedFromEntities.Add(keyphrase);
//						}
//					}
//				}
//			}

//			// at this point, if any of the automatically extracted keyphrases match those added manually, those manually 
//			// added got their docFreq increased; remaining automatically extracted keyphrases are added to the list of 
//			// automatically extracted keyphrases from entities

//			// should we really care about keyphrases that were deleted by our user from our entities?
//			// as we discussed this before, we don't care about that. If they were deleted (and they could be deleted only
//			// manually), they were deleted. There is only one way to say that a given keyphrase shouldn't be a part of the cluster 
//			// itself: by explicitly marking it as deleted at the cluster level.
//			// Thus, we don't do anything else with the remaining keyphrases.

//			// we have two collections of meaningful keyphrases from entities: those manually added (we believe 
//			// they should be treated as more important, compared to those that were obtained automatically), and those 
//			// automatically extracted.
			
//			// we have two collections of meaningful keyphrases from the cluster itself: those manually added
//			// and those marked as deleted.
	
//			// we'll use list of deleted keyphrases as the way to filter out the keyphrases, extracted from entities

//			// the key question is how should we deal with cluster-level keyphrases?
//			// e.g., do we think that such keyphrases belong to all entities with the cluster? If so, their docFreq 
//			// should be automatically set equal to the number of entities that form the cluster
//			// otherwise, we should set their docFreq to 1, but this is wrong. Let's use the count of all entities for that.

//			// so, at the end of the day, we care only about forming a list of meaningful keyphrases, right?
//			// as we want to treat different keyphrases differently, we should keep three lists.
//			// those marked as manually added (at cluster, and at the entities level), and those marked as automatically extracted

//			/// <summary>
//			/// Keyphrases, manually added to Cluster
//			/// </summary>
//			var tier1 = new List<Keyphrase>(); 

//			/// <summary>
//			/// Keyphrases, manually added to Entities
//			/// </summary>
//			var tier2 = new List<Keyphrase>();

//			/// <summary>
//			/// Keyphrases, automatically extracted from Entities
//			/// </summary>
//			var tier3 = new List<Keyphrase>();

//			// first, let's filter automatically extracted keyphrases out any marked as deleted at the cluster level
//			foreach (var keyphrase in extAddedFromEntities)
//			{
//				var markedAsDeletedAtCluster = deletedFromCluster.Where(k=>k.Phrase.ToLower() == keyphrase.Phrase.ToLower()).FirstOrDefault();

//				if (markedAsDeletedAtCluster!=null)
//				{
//					// we skip it
//				}
//				else
//				{
//					// we add it to tier3
//					tier3.Add(keyphrase); // this will also take its docFreq into account
//				}
//			}

//			// second, let's filter out manually added keyphrases out of any marked as deleted at the cluster level
//			// this is questionably, though
//			foreach (var keyphrase in manAddedFromEntities)
//			{
//				var markedAsDeletedAtCluster = deletedFromCluster.Where(k=>k.Phrase.ToLower() == keyphrase.Phrase.ToLower()).FirstOrDefault();

//				if (markedAsDeletedAtCluster!=null)
//				{
//					// we skip it
//				}
//				else
//				{
//					// we add it to tier2
//					tier2.Add(keyphrase); // this will also take its docFreq into account
//				}
//			}

//			var countOfEntities = entities.Count;

//			// finally, let's add all manually added at the cluster level to tier 1, and update their docFreq
//			foreach (var keyphrase in addedToCluster)
//			{
//				keyphrase.DocFreq = countOfEntities;

//				tier1.Add(keyphrase);
//			}

//			// now, could it be that any of the keyphrases that were automatically or manually extracted at the entity
//			// level, were also added manually at the cluster level?
//			// e.g., "Doc1" has "Keyphrase1", and user manually added "Keyphrase1" to the cluster
//			// what should we do with that?
//			// well, we could do the following:
//			// we could mark such keyphrases as manually added (thus as most important ones). This would allow us 
//			// to finally have one list of ALL keyphrases; for it, we could finally calculate updated ranks of each 
//			// keyphrase, and return that list to the requestor

//			// this one is based on tier1
//			var preFinalList = new List<Keyphrase>(tier1);

//			foreach (var keyphrase in tier3)
//			{
//				var kInTier1 = preFinalList.Where(k=>k.Phrase.ToLower() == keyphrase.Phrase.ToLower()).FirstOrDefault();

//				if (kInTier1!=null)
//				{
//					// we don't add it, but change its rank
//					if (kInTier1.Rank < keyphrase.Rank)
//						kInTier1.Rank = keyphrase.Rank;
//				}
//				else
//				{
//					// we add it
//					preFinalList.Add(keyphrase);
//				}
//			}

//			foreach (var keyphrase in tier2)
//			{
//				var kInPreFinal = preFinalList.Where(k=>k.Phrase.ToLower() == keyphrase.Phrase.ToLower()).FirstOrDefault();

//				if (kInPreFinal != null)
//				{
//					// we don't add it, but change its rank
//					if (kInPreFinal.Rank < keyphrase.Rank)
//						kInPreFinal.Rank = keyphrase.Rank;
//				}
//				else
//				{
//					// we add it
//					preFinalList.Add(keyphrase);
//				}
//			}

//			// at this point, we should be ready. DocFreq is either set to count of all entities (for those added at the cluster level),
//			// or to the # of entities that have that specific keyphrase 

//			// but now we need to remove all those keyphrases, that are not significant, right?
//			// DocFreq(Term term): Returns the number of documents containing the term.
//			// this means that we need to have the following structure: 

//			// next step: using modified algorithm from HighFreqTerms to cut the pre-final list 
//			TermInfoQueue<TermInfo> tiq = new TermInfoQueue<TermInfo>(numberOfTerms);

//			int minFreq = 0;

//			//if (preFinalList.Count == 1)
//			//{
//			//	var keyphrase=  preFinalList.FirstOrDefault();
//			//	keyphrase.DocFreq = 1;
//			//}

//			// now we do the same algorithm as in HIghFreqTerms, but adapted to our Visual Cluster selection of items
//			foreach (var term in preFinalList)
//			{
//				// Re-constructing Lucene's Term from the given text
//				Term t = new Term(Constants.Indexers.FullTextSearchIndexer.FullText, term.Phrase);

//				// terms.DocFreq()
//				if (term.DocFreq > minFreq)
//				{
//					TermInfo top = (TermInfo)tiq.Add(new TermInfo(t, term.DocFreq));
//					if (tiq.Size() >= numberOfTerms) 		     // if tiq overfull
//					{
//						tiq.Pop();				     // remove lowest in tiq
//						minFreq = top.DocFreq; // reset minFreq
//					}
//				}
//			}

//			// finally
//			TermInfo[] results = new TermInfo[tiq.Size()];

//			for (int i = 0; i < results.Length; i++)
//			{
//				results[results.Length - i - 1] = (TermInfo)tiq.Pop();
//			}

//			// this is nice, but we need to cut keyphrases from the preFinalList...
//			foreach (var result in results)
//			{
//				var keyphrase = preFinalList.Where(k => k.Phrase.ToLower() == result.Term.Text.ToLower()).FirstOrDefault();
//				if (keyphrase != null)
//				{
//					termInfo.Add(keyphrase);
//				}
//			}
//			// ready!
			
//			return true;
//		}
	
//	} // class
//} // namespace
