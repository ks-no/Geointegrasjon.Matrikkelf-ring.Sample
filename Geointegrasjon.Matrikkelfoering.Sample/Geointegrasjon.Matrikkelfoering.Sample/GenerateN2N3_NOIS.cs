using no.geointegrasjon.rep.matrikkelfoering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using no.geointegrasjon.rep.matrikkelfoering;

namespace Geointegrasjon.Matrikkelfoering.SendSample
{
    class GenerateN2N3_NOIS
    {

        /// <summary>
        /// Nivå 2 - Beskjed om godkjent vedtak på ett trinn søknad - med gjeldende tegninger og utfylt matrikkelopplysninger på ny enebolig
        ///Brukstilfelle matrikkel - 8.6.2 Nybygg, nytt bygg - igangsettingstillatelse gitt  (denne brukes for ett trinn)
        ///Søknaden kan etterfølges med endringssøknad, midlertidig brukstillatelse eller ferdigattest
        ///Mer om søknaden - https://dibk-utvikling.atlassian.net/wiki/spaces/FB/pages/31129622/S+knad+om+tillatelse+i+ett+trinn
        /// </summary>
        /// <returns>Byggesak med utfylt matrikkelopplysninger for ny enebolig</returns>
        public ByggesakType GenerateSampleRetorten(bool withMatrikkelOpplysninger = true)
        {


            var byggesak = new ByggesakType();
            byggesak.adresse = "Gunnerusgate 1";
            byggesak.tittel = "Ett trinn søknad for enebolig i Gunnerusgate 1";
            //byggesak.tittel = "Rammetillatelse for enebolig i Gunnerusgate 1 Arne";
            //byggesak.tittel = "Søknad om tiltak uten ansvarsrett for enebolig i g i Gunnerusgate 1 Arne";

            // LARS
            var posisjon = new Punkt();
            var koordinat = new Koordinat();
            koordinat.x = 569285; // Fra Arne
            koordinat.y = 7034028;
            //koordinat.x = 569287;
            //koordinat.y = 7034030;
            koordinat.zSpecified = false;

            posisjon.posisjon = koordinat;

            var koordinatsystemKode = new KoordinatsystemKode();
            var epsgCode = "25832";
            var sosiCode = "22";
            koordinatsystemKode.kodeverdi = sosiCode;
            posisjon.koordinatsystem = koordinatsystemKode; // EPSG eller SOSI kode?

            byggesak.posisjon = posisjon;

            byggesak.saksnummer = new SaksnummerType() { saksaar = "2019", sakssekvensnummer = "1234567" };

            byggesak.kategori = new ProsesskategoriType() { kode = "ET", beskrivelse = "Søknad om tillatelse i ett trinn" };
            //byggesak.kategori = new ProsesskategoriType() { kode = "RS", beskrivelse = "Søknad om rammetillatelse" };
            //byggesak.kategori = new ProsesskategoriType() { kode = "TA", beskrivelse = "Søknad om tiltak uten ansvarsrett" };
            byggesak.tiltakstype = new[] { new TiltaktypeType() { kode = "nyttbyggboligformal", beskrivelse = "Nytt bygg - boligformål" } };
            byggesak.vedtak = new VedtakType() { beskrivelse = "Vedtak om byggetillatelse", status = new VedtakstypeType() { kode = "1", beskrivelse = "Godkjent" }, vedtaksdato = DateTime.Now };

            PartType ansvarligSoeker = new PartType();
            ansvarligSoeker.organisasjonsnummer = "944101233"; // Dette organisasjonsnummer må finnes i virkeligheten, eksempel for NOIS
            byggesak.ansvarligSoeker = ansvarligSoeker;

            if (!withMatrikkelOpplysninger)
            {
                return byggesak; // create Byggesak without matrikkelopplysninger
            }

            byggesak.matrikkelopplysninger = new MatrikkelopplysningerType();
            byggesak.matrikkelopplysninger.adresse = new[]
            {
                new AdresseType() {adressekode = "2470", adressenavn = "Gunnerusgate", adressenummer = "1"},
                new AdresseType() {adressekode = "2470", adressenavn = "Gunnerusgate", adressenummer = "3"} //Arne
            };
            byggesak.matrikkelopplysninger.eiendomsidentifikasjon = new[]
            {
                new MatrikkelnummerType() {kommunenummer = "5001", gaardsnummer = "403", bruksnummer = "77",festenummer = "1",seksjonsnummer = "2"}, //Arne
                new MatrikkelnummerType() {kommunenummer = "5001", gaardsnummer = "403", bruksnummer = "77"}
            };

            // Fra Arne:
            //PartType ansvarligSoeker = new PartType();
            //ansvarligSoeker.organisasjonsnummer = "944101233"; // Dette organisasjonsnummer må finnes i virkeligheten, eksempel for NOIS
            //ansvarligSoeker.navn = "ansvarlig navn";
            //ansvarligSoeker.adresse = new EnkelAdresseType()
            //{
            //    adresselinje1 = "Storgata 20",
            //    landkode = "NO",
            //    postnr = "5020",
            //    poststed = "Bergen"
            //};
            //byggesak.ansvarligSoeker = ansvarligSoeker;

            PartType tiltakshaver = new PartType();
            tiltakshaver.organisasjonsnummer = "944101233"; // Dette organisasjonsnummer mÃ¥ finnes i virkeligheten, eksempel for NOIS
            byggesak.tiltakshaver = tiltakshaver;



            byggesak.matrikkelopplysninger.bygning = new[]
                {
                    new BygningType
                    {
                        bebygdAreal = 100,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "111",
                            beskrivelse = "Enebolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 100,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 100,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            },
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "U",
                                    beskrivelse = "U"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 100,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 100,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "U",
                                        beskrivelse = "U"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "1",
                                    beskrivelse = "Kjøkken"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = ""

                                },
                                endring =  EndringsstatusType.Ny,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "AnnenPrivatInnlagt", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Annen privat vannforsyning, innlagt vann"
                        },

                        energiforsyning = new EnergiforsyningType()
                        {
                            varmefordeling = new[]
                            {
                                new VarmefordelingType()
                                {
                                    kode = "elektriskePanelovner", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/varmefordeling
                                    beskrivelse = "Elektriske panelovner"
                                },
                                new VarmefordelingType()
                                {
                                    kode = "elektriskeVarmekabler",
                                    beskrivelse = "Elektriske varmekabler"
                                },
                            },
                            energiforsyning = new[]
                            {
                                new EnergiforsyningTypeType()
                                {
                                    kode = "biobrensel", // ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/energiforsyningtype
                                    beskrivelse = "Biobrensel"
                                },
                                new EnergiforsyningTypeType()
                                {
                                    kode = "elektrisitet",
                                    beskrivelse = "Elektrisitet"
                                },
                            },
                            relevant = true,
                            relevantSpecified = true
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    }, // Fra Arne
                    
                    new BygningType
                    {
                        bebygdAreal = 25,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "181",
                            beskrivelse = "Garasje" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "Y",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 25,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 25,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 0,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {
                                //// Unummererte bruksenheter har ikke bruksenhetsnummer i matrikkelen
                                //bruksenhetsnummer = new BruksenhetsnummerType
                                //{
                                //    etasjeplan = new EtasjeplanType
                                //    {
                                //        kode = "U",
                                //        beskrivelse = "U"
                                //    },
                                //    etasjenummer = "01",
                                //    loepenummer = "01"
                                //},
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "U",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Kjøkken"
                                },
                               adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = ""

                                },
                                endring =  EndringsstatusType.Ny,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            }
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    }
            };




            return byggesak;

        }

        /// <summary>
        /// Nivå 2 - Beskjed om godkjent vedtak på ett trinn søknad - med gjeldende tegninger og utfylt matrikkelopplysninger på ny enebolig
        ///Brukstilfelle matrikkel - 8.6.2 Nybygg, nytt bygg - igangsettingstillatelse gitt  (denne brukes for ett trinn)
        ///Søknaden kan etterfølges med endringssøknad, midlertidig brukstillatelse eller ferdigattest
        ///Mer om søknaden - https://dibk-utvikling.atlassian.net/wiki/spaces/FB/pages/31129622/S+knad+om+tillatelse+i+ett+trinn
        /// </summary>
        /// <returns>Byggesak med utfylt matrikkelopplysninger for ny enebolig</returns>
        public ByggesakType GenerateSample()
        {

            var byggesak = new ByggesakType();
            byggesak.adresse = "Byggestedgate 1";
            byggesak.tittel = "Ett trinn søknad for enebolig i Byggestedgate 1";



            byggesak.saksnummer = new SaksnummerType() { saksaar = "2019", sakssekvensnummer = "123456" };
            //  byggesak.saksnummer = new SaksnummerType() { saksaar = "2018", sakssekvensnummer = "123456" };

            byggesak.kategori = new ProsesskategoriType() { kode = "ET", beskrivelse = "Søknad om tillatelse i ett trinn" };
            byggesak.tiltakstype = new[] { new TiltaktypeType() { kode = "nyttbyggboligformal", beskrivelse = "Nytt bygg - boligformål" } };
            byggesak.vedtak = new VedtakType() { beskrivelse = "Vedtak om byggetillatelse", status = new VedtakstypeType() { kode = "1", beskrivelse = "Godkjent" }, vedtaksdato = DateTime.Now };

            byggesak.matrikkelopplysninger = new MatrikkelopplysningerType();
            byggesak.matrikkelopplysninger.adresse = new[]
            {
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "1"}
            };
            byggesak.matrikkelopplysninger.eiendomsidentifikasjon = new[]
            {
                new MatrikkelnummerType() {kommunenummer = "9999", gaardsnummer = "1", bruksnummer = "2"}
            };
            byggesak.matrikkelopplysninger.bygning = new[]
                {
                    new BygningType
                    {
                        bebygdAreal = 100,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "111",
                            beskrivelse = "Enebolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 100,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 100,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1234",
                                    adressenavn = "Gatenavn",
                                    adressenummer = "1",
                                    adressebokstav = "A"

                                },
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "AnnenPrivatInnlagt", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Annen privat vannforsyning, innlagt vann"
                        },

                        energiforsyning = new EnergiforsyningType()
                        {
                            varmefordeling = new[]
                            {
                                new VarmefordelingType()
                                {
                                    kode = "elektriskePanelovner", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/varmefordeling
                                    beskrivelse = "Elektriske panelovner"
                                },
                                new VarmefordelingType()
                                {
                                    kode = "elektriskeVarmekabler",
                                    beskrivelse = "Elektriske varmekabler"
                                },
                            },
                            energiforsyning = new[]
                            {
                                new EnergiforsyningTypeType()
                                {
                                    kode = "biobrensel", // ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/energiforsyningtype
                                    beskrivelse = "Biobrensel"
                                },
                                new EnergiforsyningTypeType()
                                {
                                    kode = "elektrisitet",
                                    beskrivelse = "Elektrisitet"
                                },
                            },
                            relevant = true,
                            relevantSpecified = true
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    }
            };




            return byggesak;
        }

        /// <summary>
        ///Nivå 2 - Beskjed om godkjent vedtak på rammesøknad - med mange adresser
        ///Brukstilfelle matrikkel - 8.6.1 Nybygg, nytt bygg - rammetillatelse gitt
        ///Søknaden må etterfølges med minst en igangsettingssøknad
        ///Mer om søknaden - https://dibk-utvikling.atlassian.net/wiki/spaces/FB/pages/31129620/S+knad+om+rammetillatelse
        /// </summary>
        /// <returns>Byggesak med utfylt matrikkelopplysninger for oppføring av 5 tomannsboliger</returns>
        public ByggesakType GenerateSample2()
        {

            var byggesak = new ByggesakType();
            byggesak.adresse = "Byggestedgate 3 - 11";
            byggesak.tittel = "Rammesøknad for oppføring av 5 tomannsboliger";
            byggesak.saksnummer = new SaksnummerType() { saksaar = "2018", sakssekvensnummer = "123456" };
            byggesak.kategori = new ProsesskategoriType() { kode = "RS", beskrivelse = "Søknad om rammetillatelse" };
            byggesak.tiltakstype = new[] { new TiltaktypeType() { kode = "nyttbyggboligformal", beskrivelse = "Nytt bygg - boligformål" } };
            byggesak.vedtak = new VedtakType() { beskrivelse = "Vedtak om rammetillatelse", status = new VedtakstypeType() { kode = "1", beskrivelse = "Godkjent" }, vedtaksdato = DateTime.Now };
            byggesak.saksbehandler = "Michael";
            byggesak.ansvarligSoeker = new PartType() { navn = "Arkitekt Flink", organisasjonsnummer = "123456789" };
            byggesak.tiltakshaver = new PartType() { navn = "Hans Utbygger", foedselsnummer = "12345678901" };

            byggesak.matrikkelopplysninger = new MatrikkelopplysningerType();
            byggesak.matrikkelopplysninger.adresse = new[]
            {
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "3", adressebokstav="A"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "3", adressebokstav="B"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "5", adressebokstav="A"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "5", adressebokstav="B"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "7", adressebokstav="A"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "7", adressebokstav="B"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "9", adressebokstav="A"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "9", adressebokstav="B"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "11", adressebokstav="A"},
                new AdresseType() {adressekode = "1001", adressenavn = "Byggestedgate", adressenummer = "11", adressebokstav="B"},
            };
            byggesak.matrikkelopplysninger.eiendomsidentifikasjon = new[]
            {
                new MatrikkelnummerType() {kommunenummer = "9999", gaardsnummer = "260", bruksnummer = "109"}
            };
            byggesak.matrikkelopplysninger.bygning = new[]
                {
                    new BygningType
                    {
                        bebygdAreal = 100,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "121",
                            beskrivelse = "Tomannsbolig, vertikaldelt" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 200,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 200,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "3",
                                    adressebokstav = "A"

                                },
                                endring =  EndringsstatusType.Ny
                            },
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "3",
                                    adressebokstav = "B"

                                },
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "TilknyttetOffVannverk", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Offentlig vannverk"
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    },
                    new BygningType
                    {
                        bebygdAreal = 100,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "121",
                            beskrivelse = "Tomannsbolig, vertikaldelt" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 200,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 200,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "5",
                                    adressebokstav = "A"

                                },
                                endring =  EndringsstatusType.Ny
                            },
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "5",
                                    adressebokstav = "B"

                                },
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "TilknyttetOffVannverk", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Offentlig vannverk"
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    },
                    new BygningType
                    {
                        bebygdAreal = 100,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "121",
                            beskrivelse = "Tomannsbolig, vertikaldelt" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 200,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 200,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "7",
                                    adressebokstav = "A"

                                },
                                endring =  EndringsstatusType.Ny
                            },
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "7",
                                    adressebokstav = "B"

                                },
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "TilknyttetOffVannverk", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Offentlig vannverk"
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    },
                    new BygningType
                    {
                        bebygdAreal = 100,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "121",
                            beskrivelse = "Tomannsbolig, vertikaldelt" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 200,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 200,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "9",
                                    adressebokstav = "A"

                                },
                                endring =  EndringsstatusType.Ny
                            },
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "9",
                                    adressebokstav = "B"

                                },
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "TilknyttetOffVannverk", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Offentlig vannverk"
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    },
                    new BygningType
                    {
                        bebygdAreal = 100,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "121",
                            beskrivelse = "Tomannsbolig, vertikaldelt" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 200,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 200,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "11",
                                    adressebokstav = "A"

                                },
                                endring =  EndringsstatusType.Ny
                            },
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "H",
                                        beskrivelse = "H"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Ikke oppgitt"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "1001",
                                    adressenavn = "Byggestedgate",
                                    adressenummer = "11",
                                    adressebokstav = "B"

                                },
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "TilknyttetOffVannverk", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Offentlig vannverk"
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    }
            };




            return byggesak;
        }
        public ByggesakType GenerateSample1()
        {
            var byggesak = new ByggesakType();
            byggesak.adresse = "Gunnerusgate 1";
            byggesak.tittel = "Endringssøknad for enebolig i Gunnerusgate 1 Arne 2";
            byggesak.tittel = "Søknad om igangsettingtillatelsei Gunnerusgate 1 Arne";
            byggesak.tittel = "Søknad om midlertidig brukstillatelse Gunnerusgate 1 Arne";
            byggesak.tittel = "Søknad om ferdigattest Gunnerusgate 1 Arne";
            byggesak.saksnummer = new SaksnummerType() { saksaar = "2018", sakssekvensnummer = "123456" };
            byggesak.kategori = new ProsesskategoriType() { kode = "ES", beskrivelse = "Søknad om endring av tillatelse" };
            byggesak.kategori = new ProsesskategoriType() { kode = "IG", beskrivelse = "Søknad om igangsettingtillatelse" };
            byggesak.kategori = new ProsesskategoriType() { kode = "MB", beskrivelse = "Søknad om midlertidig brukstillatelse" };
            byggesak.kategori = new ProsesskategoriType() { kode = "FA", beskrivelse = "Søknad om ferdigattest" };
            byggesak.tiltakstype = new[] { new TiltaktypeType() { kode = "nyttbyggboligformal", beskrivelse = "Nytt bygg - boligformål" } };
            byggesak.vedtak = new VedtakType() { beskrivelse = "Vedtak om endring av tillatelse", status = new VedtakstypeType() { kode = "1", beskrivelse = "Godkjent" }, vedtaksdato = DateTime.Now };

            byggesak.matrikkelopplysninger = new MatrikkelopplysningerType();
            byggesak.matrikkelopplysninger.adresse = new[]
            {
                new AdresseType() {adressekode = "2470", adressenavn = "Gunnerusgate", adressenummer = "1"}
            };
            byggesak.matrikkelopplysninger.eiendomsidentifikasjon = new[]
            {
                new MatrikkelnummerType() {kommunenummer = "5001", gaardsnummer = "403", bruksnummer = "77"}
            };

            PartType ansvarligSoeker = new PartType();
            ansvarligSoeker.organisasjonsnummer = "962392687";
            byggesak.ansvarligSoeker = ansvarligSoeker;

            PartType tiltakshaver = new PartType();
            tiltakshaver.organisasjonsnummer = "944101233";
            byggesak.tiltakshaver = tiltakshaver;
            

            string val;
            string val2;
            Console.Write("Angi byggnr: ");
            val = Console.ReadLine();

            Console.Write("Angi byggnr 2: ");
            val2 = Console.ReadLine();

            //string byggNr = val;
            byggesak.matrikkelopplysninger.bygning = new[]
                {
                    new BygningType
                    {
                        bygningsnummer =  val,
                        bebygdAreal = 110,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "112",
                            beskrivelse = "Enebolig med hybel eller sokkelleilighet" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "L",
                                    beskrivelse = "L"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 50,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 50,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            },
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 110,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 110,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            },
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "U",
                                    beskrivelse = "U"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 110,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 110,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "U",
                                        beskrivelse = "U"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "1",
                                    beskrivelse = "Kjøkken"
                                },
                                bruksareal = 100,
                                bruksarealSpecified = true,
                                antallRom = "3",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = ""

                                },
                                endring =  EndringsstatusType.Ny,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            },
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "U",
                                        beskrivelse = "U"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "02"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "1",
                                    beskrivelse = "Kjøkken"
                                },
                                bruksareal = 50,
                                bruksarealSpecified = true,
                                antallRom = "2",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = ""

                                },
                                endring =  EndringsstatusType.Ny,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            },
                            new BruksenhetType
                            {

                              
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "U",
                                    beskrivelse = "Unummerert bruksenhet"
                                },
                               adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = "B"

                                },
                                endring =  EndringsstatusType.Ny,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avløpsanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "AnnenPrivatInnlagt", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Annen privat vannforsyning, innlagt vann"
                        },

                        energiforsyning = new EnergiforsyningType()
                        {
                            varmefordeling = new[]
                            {
                                new VarmefordelingType()
                                {
                                    kode = "elektriskePanelovner", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/varmefordeling
                                    beskrivelse = "Elektriske panelovner"
                                },
                                new VarmefordelingType()
                                {
                                    kode = "elektriskeVarmekabler",
                                    beskrivelse = "Elektriske varmekabler"
                                },
                            },
                            energiforsyning = new[]
                            {
                                new EnergiforsyningTypeType()
                                {
                                    kode = "biobrensel", // ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/energiforsyningtype
                                    beskrivelse = "Biobrensel"
                                },
                                new EnergiforsyningTypeType()
                                {
                                    kode = "elektrisitet",
                                    beskrivelse = "Elektrisitet"
                                },
                            },
                            relevant = true,
                            relevantSpecified = true
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    },
                    new BygningType
                    {
                        bygningsnummer =  val2,
                        bebygdAreal = 25,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "181",
                            beskrivelse = "Garasje" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "Y",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 25,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 25,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 0,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Ny
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "U",
                                        beskrivelse = "U"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "U",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "0",
                                    beskrivelse = "Kjøkken"
                                },
                               adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = ""

                                },
                                endring =  EndringsstatusType.Ny,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            }
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Ny
                    }
            };


            return byggesak;
        }

        public ByggesakType SlettEtasjeOgBruksenhet()
        {
            var byggesak = new ByggesakType();
            byggesak.adresse = "Gunnerusgate 1";
            byggesak.tittel = "EndringssÃ¸knad for enebolig i Gunnerusgate 1 Arne 2 Slette etaste og bruksenhet";
            // byggesak.tittel = "SÃ¸knad om igangsettingtillatelsei Gunnerusgate 1 Marit";
            // byggesak.tittel = "SÃ¸knad om midlertidig brukstillatelse Gunnerusgate 1 Arne";
            // byggesak.tittel = "SÃ¸knad om ferdigattest Gunnerusgate 1 Arne";
            byggesak.saksnummer = new SaksnummerType() { saksaar = "2018", sakssekvensnummer = "123456" };
            byggesak.kategori = new ProsesskategoriType() { kode = "ES", beskrivelse = "SÃ¸knad om endring av tillatelse" };
            //   byggesak.kategori = new ProsesskategoriType() { kode = "IG", beskrivelse = "SÃ¸knad om igangsettingtillatelse" };
            //    byggesak.kategori = new ProsesskategoriType() { kode = "MB", beskrivelse = "SÃ¸knad om midlertidig brukstillatelse" };
            //  byggesak.kategori = new ProsesskategoriType() { kode = "FA", beskrivelse = "SÃ¸knad om ferdigattest" };
            byggesak.tiltakstype = new[] { new TiltaktypeType() { kode = "nyttbyggboligformal", beskrivelse = "Nytt bygg - boligformÃ¥l" } };
            byggesak.vedtak = new VedtakType() { beskrivelse = "Vedtak om endring av tillatelse", status = new VedtakstypeType() { kode = "1", beskrivelse = "Godkjent" }, vedtaksdato = DateTime.Now };

            byggesak.matrikkelopplysninger = new MatrikkelopplysningerType();
            byggesak.matrikkelopplysninger.adresse = new[]
            {
                new AdresseType() {adressekode = "2470", adressenavn = "Gunnerusgate", adressenummer = "1"},
            };
            byggesak.matrikkelopplysninger.eiendomsidentifikasjon = new[]
            {
                new MatrikkelnummerType() {kommunenummer = "5001", gaardsnummer = "403", bruksnummer = "77"}
            };

            PartType ansvarligSoeker = new PartType();
            ansvarligSoeker.organisasjonsnummer = "962392687";
            byggesak.ansvarligSoeker = ansvarligSoeker;

            PartType tiltakshaver = new PartType();
            tiltakshaver.organisasjonsnummer = "944101233";
            byggesak.tiltakshaver = tiltakshaver;


            string val;
            string val2;
            Console.Write("Angi byggnr: ");
            val = Console.ReadLine();


#if true
            //string byggNr = val;
            byggesak.matrikkelopplysninger.bygning = new[]
                {
                    new BygningType
                    {
                        bygningsnummer =  val,
                        bebygdAreal = 110,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "112",
                            beskrivelse = "Enebolig med hybel eller sokkelleilighet" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "X",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "L",
                                    beskrivelse = "L"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 50,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 0,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 50,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Fjernet
                            }
                        },
                        bruksenheter = new[]
                        {

                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "U",
                                        beskrivelse = "U"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "02"
                                },
                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "B",
                                    beskrivelse = "Bolig"
                                },
                                kjoekkentilgang = new KjoekkentilgangKodeType
                                {
                                    kode = "1",
                                    beskrivelse = "KjÃ¸kken"
                                },
                                bruksareal = 50,
                                bruksarealSpecified = true,
                                antallRom = "2",
                                antallBad = "1",
                                antallWC = "1",
                                adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = ""

                                },
                                endring =  EndringsstatusType.Fjernet,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            },
                            new BruksenhetType
                            {


                                bruksenhetstype = new BruksenhetstypeKodeType
                                {
                                    kode = "U",
                                    beskrivelse = "Unummerert bruksenhet"
                                },
                               adresse = new AdresseType
                                {
                                    adressekode = "2470",
                                    adressenavn = "Gunnerus gate",
                                    adressenummer = "1",
                                    adressebokstav = ""

                                },
                                endring =  EndringsstatusType.Fjernet,
                                matrikkelnummer = new MatrikkelnummerType
                                {
                                    kommunenummer = "5001",
                                    gaardsnummer = "403",
                                    bruksnummer = "177"
                                }
                            }
                        },
                        avlop = new AvloepstilknytningType()
                        {
                            kode = "OffentligKloakk",
                            beskrivelse = "Offentlig avlÃ¸psanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                        },

                        vannforsyning = new VanntilknytningType()
                        {
                            kode = "AnnenPrivatInnlagt", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                            beskrivelse = "Annen privat vannforsyning, innlagt vann"
                        },

                        energiforsyning = new EnergiforsyningType()
                        {
                            varmefordeling = new[]
                            {
                                new VarmefordelingType()
                                {
                                    kode = "elektriskePanelovner", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/varmefordeling
                                    beskrivelse = "Elektriske panelovner"
                                },
                                new VarmefordelingType()
                                {
                                    kode = "elektriskeVarmekabler",
                                    beskrivelse = "Elektriske varmekabler"
                                },
                            },
                            energiforsyning = new[]
                            {
                                new EnergiforsyningTypeType()
                                {
                                    kode = "biobrensel", // ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/energiforsyningtype
                                    beskrivelse = "Biobrensel"
                                },
                                new EnergiforsyningTypeType()
                                {
                                    kode = "elektrisitet",
                                    beskrivelse = "Elektrisitet"
                                },
                            },
                            relevant = true,
                            relevantSpecified = true
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Endret
                    }
            };
#endif

            return byggesak;
        }

        public ByggesakType RivGarasjeByggEnebolig()
        {
            var byggesak = new ByggesakType();
            byggesak.adresse = "Gunnerusgate 1";
            byggesak.tittel = "RivGarasjeByggEnebolig  ";


            // LARS
            var posisjon = new Punkt();
            var koordinat = new Koordinat();
            koordinat.x = 569285;
            koordinat.y = 7034028;
            koordinat.zSpecified = false;

            posisjon.posisjon = koordinat;

            var koordinatsystemKode = new KoordinatsystemKode();
            var epsgCode = "25832";
            var sosiCode = "22";
            koordinatsystemKode.kodeverdi = sosiCode;
            posisjon.koordinatsystem = koordinatsystemKode; // EPSG eller SOSI kode?

            byggesak.posisjon = posisjon;

            byggesak.saksnummer = new SaksnummerType() { saksaar = "2019", sakssekvensnummer = "1234567" };

            byggesak.kategori = new ProsesskategoriType() { kode = "ET", beskrivelse = "SÃ¸knad om tillatelse i ett trinn" };
            //   byggesak.kategori = new ProsesskategoriType() { kode = "RS", beskrivelse = "SÃ¸knad om rammetillatelse" };
            //byggesak.kategori = new ProsesskategoriType() { kode = "TA", beskrivelse = "SÃ¸knad om tiltak uten ansvarsrett" };
            byggesak.tiltakstype = new[] { new TiltaktypeType() { kode = "nyttbyggboligformal", beskrivelse = "Nytt bygg - boligformÃ¥l" } };
            byggesak.vedtak = new VedtakType() { beskrivelse = "Vedtak om byggetillatelse", status = new VedtakstypeType() { kode = "1", beskrivelse = "Godkjent" }, vedtaksdato = DateTime.Now };

            byggesak.matrikkelopplysninger = new MatrikkelopplysningerType();
            byggesak.matrikkelopplysninger.adresse = new[]
            {
                new AdresseType() {adressekode = "2470", adressenavn = "Gunnerusgate", adressenummer = "1"},
            };
            byggesak.matrikkelopplysninger.eiendomsidentifikasjon = new[]
            {
                new MatrikkelnummerType() {kommunenummer = "5001", gaardsnummer = "403", bruksnummer = "78"}
            };

            PartType ansvarligSoeker = new PartType();
            //  ansvarligSoeker.organisasjonsnummer = "944101233"; // Dette organisasjonsnummer mÃ¥ finnes i virkeligheten, eksempel for NOIS
            //ansvarligSoeker.navn = "ansvarlig navn";
            //ansvarligSoeker.adresse = new EnkelAdresseType()
            //{
            //    adresselinje1 = "Storgata 20", landkode = "NO", postnr = "5020", poststed = "Bergen"
            //};
            //  ansvarligSoeker.foedselsnummer = "09027339988"; 
            //  byggesak.ansvarligSoeker = ansvarligSoeker;

            PartType tiltakshaver = new PartType();

            tiltakshaver.navn = "tiltakshaver navn";
            tiltakshaver.adresse = new EnkelAdresseType()
            {
                adresselinje1 = "Kongens gate 10",
                landkode = "NO",
                postnr = "513",
                poststed = "Oslo"
            };

            tiltakshaver.organisasjonsnummer = "944101233"; // Dette organisasjonsnummer mÃ¥ finnes i virkeligheten, eksempel for NOIS
            //ansvarligSoeker.foedselsnummer = "09027339988";
            byggesak.tiltakshaver = tiltakshaver;

            Console.Write("Angi byggnr som skal rives: ");
            string byggNr = Console.ReadLine();

            Console.Write("Angi 2. byggnr som skal endres: ");
            string byggNr2 = Console.ReadLine();

#if true
            byggesak.matrikkelopplysninger.bygning = new[]
                {
                    new BygningType
                    {
                        bygningsnummer = byggNr,
                        bebygdAreal = 25,
                        bebygdArealSpecified = true,
                        bygningstype = new BygningstypeType()
                        {
                            kode = "181",
                            beskrivelse = "Garasje" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                        },
                        naeringsgruppe  = new NaeringsgruppeType()
                        {
                            kode = "Y",
                            beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                        },
                        etasjer = new[]
                        {
                            new EtasjeType
                            {

                                etasjeplan = new EtasjeplanType
                                {
                                    kode = "H",
                                    beskrivelse = "H"
                                },
                                etasjenummer = "01",
                                antallBoenheter = "1",
                                bruksarealTotalt = 25,
                                bruksarealTotaltSpecified = true,
                                bruksarealTilAnnet = 25,
                                bruksarealTilAnnetSpecified = true,
                                bruksarealTilBolig = 0,
                                bruksarealTilBoligSpecified = true,
                                endring =  EndringsstatusType.Eksisterende
                            }
                        },
                        bruksenheter = new[]
                        {
                            new BruksenhetType
                            {

                                bruksenhetsnummer = new BruksenhetsnummerType
                                {
                                    etasjeplan = new EtasjeplanType
                                    {
                                        kode = "U",
                                        beskrivelse = "U"
                                    },
                                    etasjenummer = "01",
                                    loepenummer = "01"
                                },
                                endring = EndringsstatusType.Eksisterende

                            }
                        },
                        harHeis = false,
                        harHeisSpecified = true,
                        endring =  EndringsstatusType.Fjernet
                    }
                    //,

                    // new BygningType
                    //{
                    //    bygningsnummer = byggNr2,
                    //    bebygdAreal = 25,
                    //    bebygdArealSpecified = true,
                    //    bygningstype = new BygningstypeType()
                    //    {
                    //        kode = "181",
                    //        beskrivelse = "Garasje" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                    //    },
                    //    naeringsgruppe  = new NaeringsgruppeType()
                    //    {
                    //        kode = "Y",
                    //        beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                    //    },
                    //    etasjer = new[]
                    //    {
                    //        new EtasjeType
                    //        {

                    //            etasjeplan = new EtasjeplanType
                    //            {
                    //                kode = "H",
                    //                beskrivelse = "H"
                    //            },
                    //            etasjenummer = "01",
                    //            antallBoenheter = "1",
                    //            bruksarealTotalt = 25,
                    //            bruksarealTotaltSpecified = true,
                    //            bruksarealTilAnnet = 25,
                    //            bruksarealTilAnnetSpecified = true,
                    //            bruksarealTilBolig = 0,
                    //            bruksarealTilBoligSpecified = true,
                    //            endring =  EndringsstatusType.Eksisterende
                    //        }
                    //    },
                    //    bruksenheter = new[]
                    //    {
                    //        new BruksenhetType
                    //        {

                    //            bruksenhetsnummer = new BruksenhetsnummerType
                    //            {
                    //                etasjeplan = new EtasjeplanType
                    //                {
                    //                    kode = "U",
                    //                    beskrivelse = "U"
                    //                },
                    //                etasjenummer = "01",
                    //                loepenummer = "01"
                    //            },
                    //            endring = EndringsstatusType.Eksisterende

                    //        }
                    //    },
                    //    harHeis = false,
                    //    harHeisSpecified = true,
                    //    endring =  EndringsstatusType.Eksisterende
                    //}
                    //,
                    //new BygningType
                    //{
                    //    bebygdAreal = 100,
                    //    bebygdArealSpecified = true,
                    //    bygningstype = new BygningstypeType()
                    //    {
                    //        kode = "111",
                    //        beskrivelse = "Enebolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                    //    },
                    //    naeringsgruppe  = new NaeringsgruppeType()
                    //    {
                    //        kode = "X",
                    //        beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                    //    },
                    //    etasjer = new[]
                    //    {
                    //        new EtasjeType
                    //        {

                    //            etasjeplan = new EtasjeplanType
                    //            {
                    //                kode = "H",
                    //                beskrivelse = "H"
                    //            },
                    //            etasjenummer = "01",
                    //            antallBoenheter = "1",
                    //            bruksarealTotalt = 100,
                    //            bruksarealTotaltSpecified = true,
                    //            bruksarealTilAnnet = 0,
                    //            bruksarealTilAnnetSpecified = true,
                    //            bruksarealTilBolig = 100,
                    //            bruksarealTilBoligSpecified = true,
                    //            endring =  EndringsstatusType.Ny
                    //        },
                    //        new EtasjeType
                    //        {

                    //            etasjeplan = new EtasjeplanType
                    //            {
                    //                kode = "U",
                    //                beskrivelse = "U"
                    //            },
                    //            etasjenummer = "01",
                    //            antallBoenheter = "1",
                    //            bruksarealTotalt = 100,
                    //            bruksarealTotaltSpecified = true,
                    //            bruksarealTilAnnet = 0,
                    //            bruksarealTilAnnetSpecified = true,
                    //            bruksarealTilBolig = 100,
                    //            bruksarealTilBoligSpecified = true,
                    //            endring =  EndringsstatusType.Ny
                    //        }
                    //    },
                    //    bruksenheter = new[]
                    //    {
                    //        new BruksenhetType
                    //        {

                    //            bruksenhetsnummer = new BruksenhetsnummerType
                    //            {
                    //                etasjeplan = new EtasjeplanType
                    //                {
                    //                    kode = "U",
                    //                    beskrivelse = "U"
                    //                },
                    //                etasjenummer = "01",
                    //                loepenummer = "01"
                    //            },
                    //            bruksenhetstype = new BruksenhetstypeKodeType
                    //            {
                    //                kode = "B",
                    //                beskrivelse = "Bolig"
                    //            },
                    //            kjoekkentilgang = new KjoekkentilgangKodeType
                    //            {
                    //                kode = "1",
                    //                beskrivelse = "KjÃ¸kken"
                    //            },
                    //            bruksareal = 100,
                    //            bruksarealSpecified = true,
                    //            antallRom = "3",
                    //            antallBad = "1",
                    //            antallWC = "1",
                    //            adresse = new AdresseType
                    //            {
                    //                adressekode = "2470",
                    //                adressenavn = "Gunnerus gate",
                    //                adressenummer = "1",
                    //                adressebokstav = ""

                    //            },
                    //            endring =  EndringsstatusType.Ny,
                    //            matrikkelnummer = new MatrikkelnummerType
                    //            {
                    //                kommunenummer = "5001",
                    //                gaardsnummer = "403",
                    //                bruksnummer = "177"
                    //            }
                    //        }
                    //    },
                    //    avlop = new AvloepstilknytningType()
                    //    {
                    //        kode = "OffentligKloakk",
                    //        beskrivelse = "Offentlig avlÃ¸psanlegg" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/avlopstilknytning
                    //    },

                    //    vannforsyning = new VanntilknytningType()
                    //    {
                    //        kode = "AnnenPrivatInnlagt", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/vanntilknytning 
                    //        beskrivelse = "Annen privat vannforsyning, innlagt vann"
                    //    },

                    //    energiforsyning = new EnergiforsyningType()
                    //    {
                    //        varmefordeling = new[]
                    //        {
                    //            new VarmefordelingType()
                    //            {
                    //                kode = "elektriskePanelovner", //ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/varmefordeling
                    //                beskrivelse = "Elektriske panelovner"
                    //            },
                    //            new VarmefordelingType()
                    //            {
                    //                kode = "elektriskeVarmekabler",
                    //                beskrivelse = "Elektriske varmekabler"
                    //            },
                    //        },
                    //        energiforsyning = new[]
                    //        {
                    //            new EnergiforsyningTypeType()
                    //            {
                    //                kode = "biobrensel", // ref https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/energiforsyningtype
                    //                beskrivelse = "Biobrensel"
                    //            },
                    //            new EnergiforsyningTypeType()
                    //            {
                    //                kode = "elektrisitet",
                    //                beskrivelse = "Elektrisitet"
                    //            },
                    //        },
                    //        relevant = true,
                    //        relevantSpecified = true
                    //    },
                    //    harHeis = false,
                    //    harHeisSpecified = true,
                    //    endring =  EndringsstatusType.Ny
                    //}
                    //,new BygningType
                    //{
                    //    bygningsnummer = byggNr,
                    //    bebygdAreal = 25,
                    //    bebygdArealSpecified = true,
                    //    bygningstype = new BygningstypeType()
                    //    {
                    //        kode = "181",
                    //        beskrivelse = "Garasje" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/bygningstype
                    //    },
                    //    naeringsgruppe  = new NaeringsgruppeType()
                    //    {
                    //        kode = "Y",
                    //        beskrivelse = "Bolig" //ref kodeliste https://register.geonorge.no/subregister/byggesoknad/direktoratet-for-byggkvalitet/naeringsgruppe
                    //    },
                    //    etasjer = new[]
                    //    {
                    //        new EtasjeType
                    //        {

                    //            etasjeplan = new EtasjeplanType
                    //            {
                    //                kode = "H",
                    //                beskrivelse = "H"
                    //            },
                    //            etasjenummer = "01",
                    //            antallBoenheter = "1",
                    //            bruksarealTotalt = 25,
                    //            bruksarealTotaltSpecified = true,
                    //            bruksarealTilAnnet = 25,
                    //            bruksarealTilAnnetSpecified = true,
                    //            bruksarealTilBolig = 0,
                    //            bruksarealTilBoligSpecified = true,
                    //            endring =  EndringsstatusType.Eksisterende
                    //        }
                    //    },
                    //    bruksenheter = new[]
                    //    {
                    //        new BruksenhetType
                    //        {

                    //            //bruksenhetsnummer = new BruksenhetsnummerType
                    //            //{
                    //            //    etasjeplan = new EtasjeplanType
                    //            //    {
                    //            //        kode = "U",
                    //            //        beskrivelse = "U"
                    //            //    },
                    //            //    etasjenummer = "01",
                    //            //    loepenummer = "01"
                    //            //},
                    //            bruksenhetstype = new BruksenhetstypeKodeType
                    //            {
                    //                kode = "U",
                    //                beskrivelse = "U"
                    //            },
                    //            adresse = new AdresseType
                    //            {
                    //                adressekode = "2470",
                    //                adressenavn = "Gunnerus gate",
                    //                adressenummer = "1",
                    //                adressebokstav = ""

                    //            },
                    //            matrikkelnummer = new MatrikkelnummerType
                    //            {
                    //                kommunenummer = "5001",
                    //                gaardsnummer = "403",
                    //                bruksnummer = "177"
                    //            },
                    //            endring = EndringsstatusType.Eksisterende

                    //        }
                    //    },
                    //    harHeis = false,
                    //    harHeisSpecified = true,
                    //    endring =  EndringsstatusType.Ny
                    //}

            };



#endif
            return byggesak;

        }

    }
}
