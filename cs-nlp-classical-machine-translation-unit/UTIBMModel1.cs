﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClassicalMachineTranslation
{
    [TestClass]
    public class UTIBMModel1
    {
        [TestMethod]
        public void RunSimpleTraining()
        {
            IBMModel1 model = new IBMModel1();

            List<SimpleTrainingRecord> training_corpus = new List<SimpleTrainingRecord>();

            EnglishTokenizer tokenizer_output = new EnglishTokenizer();
            FrenchTokenizer tokenizer_input = new FrenchTokenizer();

            //training_corpus_from_f_to_e.Add(new SimpleTrainingRecord()
            //{
            //    InputLang=tokenizer_input.Tokenize("[Some French Sentence]"),
            //    OutputLang = tokenizer_output.Tokenize("[Some English Sentence]"),
            //    Alignment=new int[] { ... }
            //});
            //...

            SimpleTrainingMethod.Train(model, training_corpus);

            string sentence_input = "[Some French Sentence]";
            string sentence_output = "[Some English Sentence]";

            string[] input_lang = tokenizer_input.Tokenize(sentence_input);
            string[] output_lang = tokenizer_output.Tokenize(sentence_output);
            int[] alignment = model.GetAlignment(input_lang, output_lang);

            Dictionary<int, string> output_mapping = new Dictionary<int, string>();
            int m_input_len = input_lang.Length;
            for (int j = 0; j < m_input_len; ++j)
            {
                int a_j = alignment[j];
                string output_word = output_lang[a_j];
                output_mapping[a_j] = output_word;
            }
            List<int> output_sentence_index_list = output_mapping.Keys.ToList();
            output_sentence_index_list.Sort();

            string[] predicted_output_lang = new string[output_sentence_index_list.Count];
            for (int i = 0; i < predicted_output_lang.Length; ++i)
            {
                predicted_output_lang[i] = output_mapping[output_sentence_index_list[i]];
            }

            Console.WriteLine("Original French Sentence: {0}", sentence_input);
            Console.WriteLine("Predicted English Translation: {0}", string.Join(" ", predicted_output_lang));
        }

        [TestMethod]
        public void RunEMTraining()
        {
            IBMModel1 model = new IBMModel1();

            List<EMTrainingRecord> training_corpus = new List<EMTrainingRecord>();

            EnglishTokenizer tokenizer_output = new EnglishTokenizer();
            FrenchTokenizer tokenizer_input = new FrenchTokenizer();

            //training_corpus_from_f_to_e.Add(new EMTrainingRecord()
            //{
            //    InputLang=tokenizer_input.Tokenize("[Some French Sentence]"),
            //    OutputLang = tokenizer_output.Tokenize("[Some English Sentence]"),
            //});
            //...

            EMTrainingMethod.Train(model, training_corpus, 20);

            string sentence_input = "[Some French Sentence]";
            string sentence_output = "[Some English Sentence]";

            string[] input_lang = tokenizer_input.Tokenize(sentence_input);
            string[] output_lang = tokenizer_output.Tokenize(sentence_output);
            int[] alignment = model.GetAlignment(input_lang, output_lang);

            Dictionary<int, string> output_mapping = new Dictionary<int, string>();
            int m_input_len = input_lang.Length;
            for (int j = 0; j < m_input_len; ++j)
            {
                int a_j = alignment[j];
                string output_word = output_lang[a_j];
                output_mapping[a_j] = output_word;
            }
            List<int> output_sentence_index_list = output_mapping.Keys.ToList();
            output_sentence_index_list.Sort();

            string[] predicted_output_lang = new string[output_sentence_index_list.Count];
            for (int i = 0; i < predicted_output_lang.Length; ++i)
            {
                predicted_output_lang[i] = output_mapping[output_sentence_index_list[i]];
            }

            Console.WriteLine("Original French Sentence: {0}", sentence_input);
            Console.WriteLine("Predicted English Translation: {0}", string.Join(" ", predicted_output_lang));
        }
    }
}
