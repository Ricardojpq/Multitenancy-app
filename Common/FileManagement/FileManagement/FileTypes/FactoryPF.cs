using FileManagement.ProprietaryFile;
using System.Collections.Generic;
using FileManagement.VM;
using FileManagement.DTOs;

namespace FileManagement.FileTypes
{
    public static class FactoryPF
    {
        public static IProprietaryFile GetPf(TypeFile TypeFile, PFFormatDTO ConfInicializer = null, List<VM.PayerCrossReferenceDTO> CrossReferenceValues = null)
        {
            IProprietaryFile Pf = null;
            switch (TypeFile)
            {
                case TypeFile.Delimited:
                    Pf = new Delimiter( ConfInicializer, CrossReferenceValues);
                    break;
                case TypeFile.Fixed:
                    Pf = new Fixed ( ConfInicializer, CrossReferenceValues);
                    break;
            }
            return Pf;
        }
    }
}
