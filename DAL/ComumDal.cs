using ComercioDigitalDemoAPI.Util;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace ComercioDigitalDemoAPI.DAL
{
    public abstract class ComumDal : IDisposable
    {
        private SqlConnection _conexao;
        private SqlTransaction _transacao;

        protected SqlConnection Conexao
        {
            get { return _conexao; }
        }

        protected SqlTransaction Transacao
        {
            get { return _transacao; }
        }

        public ComumDal(bool usarTransacao)
        {
            try
            {
                var appSettingsJson = AppSettingsJson.GetAppSettings();
                _conexao = new SqlConnection(appSettingsJson["LocalSqlServerConnectionString"]);
                
                _conexao.Open();
                if(usarTransacao) _transacao = _conexao.BeginTransaction();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            try
            {
                if (Marshal.GetExceptionPointers() != IntPtr.Zero && _transacao != null
                    && _transacao.Connection.State == ConnectionState.Open)
                {
                    _transacao.Rollback();
                }
                else if (_transacao != null && Transacao.Connection.State == ConnectionState.Open)
                {
                    _transacao.Commit();
                }

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (_conexao != null)
                {
                    if (_conexao.State == ConnectionState.Open)
                        _conexao.Close();

                    _conexao.Dispose();
                }
                if (_transacao != null)
                {
                    _transacao.Dispose();
                }
            }
        }
    }
}